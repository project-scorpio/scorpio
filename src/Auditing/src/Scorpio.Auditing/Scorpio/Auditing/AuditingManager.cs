using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;
using Scorpio.Runtime;

namespace Scorpio.Auditing
{
    internal class AuditingManager : IAuditingManager, ITransientDependency
    {
        private static readonly string _ambientContextKey = "Scorpio.Auditing.IAuditScope";

        private readonly IAmbientScopeProvider<IAuditScope> _ambientScopeProvider;
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingStore _auditingStore;
        private readonly IServiceProvider _serviceProvider;
        private readonly AuditingOptions _options;
        protected ILogger<AuditingManager> Logger { get; set; }

        public IAuditScope Current => _ambientScopeProvider.GetValue(_ambientContextKey);

        public AuditingManager(
           IAmbientScopeProvider<IAuditScope> ambientScopeProvider,
           IAuditingHelper auditingHelper,
           IAuditingStore auditingStore,
           IServiceProvider serviceProvider,
           IOptions<AuditingOptions> options)
        {
            _ambientScopeProvider = ambientScopeProvider;
            _auditingHelper = auditingHelper;
            _auditingStore = auditingStore;
            _serviceProvider = serviceProvider;
            _options = options.Value;
            Logger = NullLogger<AuditingManager>.Instance;
        }

        public IAuditSaveHandle BeginScope()
        {
            var ambientScope = _ambientScopeProvider.BeginScope(
                _ambientContextKey,
                new AuditScope(_auditingHelper.CreateAuditInfo())
            );

            Debug.Assert(Current != null, "Current != null");

            return new DisposableSaveHandle(this, ambientScope, Current.Info, Stopwatch.StartNew());
        }

        protected virtual void PreSave(DisposableSaveHandle saveHandle)
        {
            saveHandle.Stopwatch.Stop();
            saveHandle.Info.ExecutionDuration = saveHandle.Stopwatch.Elapsed;
            ExecutePreContributors(saveHandle.Info);
        }

        protected virtual void PostSave(DisposableSaveHandle saveHandle) => ExecutePostContributors(saveHandle.Info);

        internal async Task SaveAsync(DisposableSaveHandle saveHandle)
        {
            PreSave(saveHandle);

            if (ShouldSave(saveHandle.Info))
            {
                await _auditingStore.SaveAsync(saveHandle.Info);
            }

            PostSave(saveHandle);
        }

        internal void Save(DisposableSaveHandle saveHandle)
        {
            PreSave(saveHandle);

            if (ShouldSave(saveHandle.Info))
            {
                _auditingStore.Save(saveHandle.Info);
            }
            PostSave(saveHandle);

        }

        protected bool ShouldSave(AuditInfo auditInfo)
        {
            if (!auditInfo.Actions.Any())
            {
                return false;
            }
            return true;
        }

        protected virtual void ExecutePostContributors(AuditInfo auditInfo)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new AuditContributionContext(scope.ServiceProvider, auditInfo);

                foreach (var contributor in _options.Contributors)
                {
                    try
                    {
                        contributor.PostContribute(context);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex, LogLevel.Warning);
                        throw;
                    }
                }
            }
        }

        protected virtual void ExecutePreContributors(AuditInfo auditInfo)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new AuditContributionContext(scope.ServiceProvider, auditInfo);

                foreach (var contributor in _options.Contributors)
                {
                    try
                    {
                        contributor.PreContribute(context);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex, LogLevel.Warning);
                        throw;
                    }
                }
            }
        }

    }
}
