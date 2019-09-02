using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeAttribute : AbstractInterceptorAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        public AuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var interceptor = context.ServiceProvider.GetService<AuthorizationInterceptor>();
            interceptor.SetPermission(Permissions,RequireAllPermissions);
            await interceptor.Invoke(context, next);
        }
    }
}
