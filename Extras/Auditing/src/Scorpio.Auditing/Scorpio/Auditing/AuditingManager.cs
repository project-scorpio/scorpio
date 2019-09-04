using AspectCore.Injector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using Scorpio.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    internal class AuditingManager : IAuditingManager, ITransientDependency
    {
        private readonly static string _ambientContextKey = "Scorpio.Auditing.IAuditScope";

        private readonly IAmbientScopeProvider<IAuditScope> _ambientScopeProvider;
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingStore _auditingStore;
        private readonly IServiceProvider _serviceProvider;
        private readonly AuditingOptions _options;
        [FromContainer]
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

        protected virtual void BeforeSave(DisposableSaveHandle saveHandle)
        {
            saveHandle.Stopwatch.Stop();
            saveHandle.Info.ExecutionDuration = saveHandle.Stopwatch.Elapsed;
            ExecutePostContributors(saveHandle.Info);
        }

        internal async Task SaveAsync(DisposableSaveHandle saveHandle)
        {
            BeforeSave(saveHandle);

            if (ShouldSave(saveHandle.Info))
            {
                await _auditingStore.SaveAsync(saveHandle.Info);
            }
        }

        internal void Save(DisposableSaveHandle saveHandle)
        {
            BeforeSave(saveHandle);

            if (ShouldSave(saveHandle.Info))
            {
                _auditingStore.Save(saveHandle.Info);
            }
        }

        protected bool ShouldSave(AuditInfo  auditInfo)
        {
            if (!auditInfo.Actions.Any() )
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
                    }
                }
            }
        }

    }
}
