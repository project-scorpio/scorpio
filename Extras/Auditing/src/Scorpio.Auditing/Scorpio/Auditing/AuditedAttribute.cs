using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditedAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var interceptor = context.ServiceProvider.GetService<AuditingInterceptor>();
            await interceptor.Invoke(context, next);
        }
    }
}
