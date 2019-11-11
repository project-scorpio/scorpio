using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Scorpio.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.AspNetCore.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuditingManager _auditingManager;

        /// <summary>
        /// 
        /// </summary>
        protected AuditingOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="auditingManager"></param>
        /// <param name="options"></param>
        public AuditingMiddleware(
            RequestDelegate next,
            IAuditingManager auditingManager,
            IOptions<AuditingOptions> options)
        {
            _next = next;
            _auditingManager = auditingManager;

            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (!ShouldWriteAuditLog(httpContext))
            {
                await _next(httpContext);
                return;
            }

            using (var scope = _auditingManager.BeginScope())
            {
                try
                {
                    await _next(httpContext);
                }
                finally
                {
                    await scope.SaveAsync();
                }
            }
        }

        private bool ShouldWriteAuditLog(HttpContext httpContext)
        {
            if (!Options.IsEnabled)
            {
                return false;
            }

            if (!Options.IsEnabledForAnonymousUsers && !(httpContext.User?.Identity.IsAuthenticated??false))
            {
                return false;
            }

            return true;
        }
    }
}
