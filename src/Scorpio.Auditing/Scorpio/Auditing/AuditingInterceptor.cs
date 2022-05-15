using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Scorpio.Aspects;
using Scorpio.DynamicProxy;
using Scorpio.Security;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingInterceptor : IInterceptor
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
        /// <param name="invocation"></param>
        /// <returns></returns>
        public async Task InterceptAsync(IMethodInvocation invocation){
             if (!ShouldIntercept(invocation, out var audit, out var auditAction))
            {
                await invocation.ProceedAsync();
                return;
            }
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await invocation.ProceedAsync();
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
        protected virtual bool ShouldIntercept(IMethodInvocation context, out AuditInfo audit, out AuditActionInfo auditAction)
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
            if (CrossCuttingConcerns.IsApplied(context.TargetObject, Concerns))
            {
                return false;
            }
            if (context.Method.AttributeExists<AuditedAttribute>())
            {
                return true;
            }
            if (context.Method.AttributeExists<DisableAuditingAttribute>())
            {
                return false;
            }

            var auditScope = _auditingManager.Current;
            if (auditScope == null)
            {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit(context.Method, true))
            {
                return false;
            }

            audit = auditScope.Info;
            auditAction = _auditingHelper.CreateAuditAction(
                context.TargetObject.GetType(), context.Method, context.Arguments
            );

            return true;
        }

    }
}
