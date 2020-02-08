using System;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Authorization
{
    /// <summary>
    /// Authorization interceptor.
    /// </summary>
    public class AuthorizationInterceptor : AbstractInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string Concern = "Scorpio.Authorization";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Scorpio.Authorization.AuthorizationInterceptor"/> class.
        /// </summary>
        public AuthorizationInterceptor()
        {
        }

        internal string[] Permissions { get; private set; }

        internal bool RequireAllPermissions { get; private set; }
        /// <summary>
        /// Invoke the specified context and next.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        /// <param name="next">Next.</param>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (Aspects.CrossCuttingConcerns.IsApplied(context.Implementation,Concern))
            {
                await next(context);
                return;
            }
            var service = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var authorizationContext = new InvocationAuthorizationContext(Permissions,RequireAllPermissions,context.ServiceMethod);
            await service.CheckAsync(authorizationContext);
            await next(context);
        }

        internal void SetPermission(string[] permissions, bool requireAllPermissions)
        {
            Permissions = permissions;
            RequireAllPermissions = requireAllPermissions;
        }
    }
}
