using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Scorpio.Aspects;
using Scorpio.Auditing;
using Scorpio.DependencyInjection;

namespace Scorpio.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="auditingHelper"></param>
        /// <param name="auditingManager"></param>
        public AuditingActionFilter(IOptions<AuditingOptions> options, IAuditingHelper auditingHelper, IAuditingManager auditingManager)
        {
            Options = options.Value;
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public AuditingOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ShouldSaveAudit(context, out var auditLog, out var auditLogAction))
            {
                await next();
                return;
            }

            using (CrossCuttingConcerns.Applying(context.Controller, AuditingInterceptor.Concerns))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    var result = await next();

                    if (result.Exception != null && !result.ExceptionHandled)
                    {
                        auditLog.Exceptions.Add(result.Exception);
                    }
                }
                catch (Exception ex)
                {
                    auditLog.Exceptions.Add(ex);
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    auditLogAction.ExecutionDuration = stopwatch.Elapsed;
                    auditLog.Actions.Add(auditLogAction);
                }
            }
        }


        private bool ShouldSaveAudit(ActionExecutingContext context, out AuditInfo auditLog, out AuditActionInfo auditLogAction)
        {
            auditLog = null;
            auditLogAction = null;

            if (!Options.IsEnabled)
            {
                return false;
            }
            if (!Options.IsAuditingController())
            {
                return false;
            }

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return false;
            }

            var auditLogScope = _auditingManager.Current;
            if (auditLogScope == null)
            {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit(context.ActionDescriptor.GetMethodInfo(), true))
            {
                return false;
            }

            auditLog = auditLogScope.Info;
            auditLogAction = _auditingHelper.CreateAuditAction(
                context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType(),
                context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo,
                context.ActionArguments
            );

            return true;
        }

    }
}
