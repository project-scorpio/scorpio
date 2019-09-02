using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Scorpio.Aspects;
using System.Reflection;
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
        public readonly static string Concerns = "Scorpio.Auditing";
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditingHelper"></param>
        /// <param name="auditingManager"></param>
        public AuditingInterceptor(IAuditingHelper auditingHelper, IAuditingManager auditingManager)
        {
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
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

            if (CrossCuttingConcerns.IsApplied(context.Implementation,Concerns))
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

            if (!_auditingHelper.ShouldSaveAudit(context.ImplementationMethod) && !_auditingHelper.ShouldSaveAudit(context.ServiceMethod))
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
