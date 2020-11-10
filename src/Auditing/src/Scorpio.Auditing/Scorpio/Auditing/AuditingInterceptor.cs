using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

using AspectCore.DynamicProxy;

using Microsoft.Extensions.Options;

using Scorpio.Aspects;
using Scorpio.Security;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingInterceptor : AbstractInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string Concerns = "Scorpio.Auditing";
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;
        private readonly ICurrentPrincipalAccessor _principalAccessor;
        private readonly AuditingOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditingHelper"></param>
        /// <param name="auditingManager"></param>
        /// <param name="options"></param>
        /// <param name="principalAccessor"></param>
        public AuditingInterceptor(IAuditingHelper auditingHelper,
            IAuditingManager auditingManager,
            IOptions<AuditingOptions> options,
            ICurrentPrincipalAccessor principalAccessor
            )
        {
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
            _principalAccessor = principalAccessor;
            _options = options.Value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (!ShouldIntercept(context, out var audit, out var auditAction))
            {
                await next(context);
                return;
            }
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                audit.Exceptions.Add(ex);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditAction.ExecutionDuration = stopwatch.Elapsed;
                audit.Actions.Add(auditAction);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="audit"></param>
        /// <param name="auditAction"></param>
        /// <returns></returns>
        protected virtual bool ShouldIntercept(AspectContext context, out AuditInfo audit, out AuditActionInfo auditAction)
        {
            audit = null;
            auditAction = null;
            if (!_options.IsEnabled)
            {
                return false;
            }
            if (!(_options.IsEnabledForAnonymousUsers || (_principalAccessor.Principal?.Identity?.IsAuthenticated ?? false)))
            {
                return false;
            }
            if (CrossCuttingConcerns.IsApplied(context.Implementation, Concerns))
            {
                return false;
            }

            if (context.ServiceMethod.AttributeExists<DisableAuditingAttribute>() || context.ImplementationMethod.AttributeExists<DisableAuditingAttribute>())
            {
                return false;
            }

            var auditScope = _auditingManager.Current;
            if (auditScope == null)
            {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit(context.ImplementationMethod, true) && !_auditingHelper.ShouldSaveAudit(context.ServiceMethod, true))
            {
                return false;
            }

            audit = auditScope.Info;
            auditAction = _auditingHelper.CreateAuditAction(
                context.Implementation.GetType(), context.ImplementationMethod, context.Parameters
            );

            return true;
        }

    }
}
