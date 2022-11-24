using Microsoft.AspNetCore.Builder;

using Scorpio.AspNetCore.Auditing;
using Scorpio.AspNetCore.Uow;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuditing(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<AuditingMiddleware>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<UnitOfWorkMiddleware>();
        }

    }
}
