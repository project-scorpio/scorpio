using System;
using System.Reflection;
using System.Threading.Tasks;


using Microsoft.Extensions.DependencyInjection;

using Scorpio.DynamicProxy;

namespace Scorpio.Authorization
{
    /// <summary>
    /// Authorization interceptor.
    /// </summary>
    internal class AuthorizationInterceptor : IInterceptor
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


        public IServiceProvider ServiceProvider { get; }

        public AuthorizationInterceptor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public async Task InterceptAsync(IMethodInvocation invocation)
        {
            if (Aspects.CrossCuttingConcerns.IsApplied(invocation.TargetObject, Concern))
            {
                await invocation.ProceedAsync();
                return;
            }
            GetPermission(invocation);
            var service = ServiceProvider.GetRequiredService<IAuthorizationService>();
            var authorizationContext = new InvocationAuthorizationContext(Permissions, RequireAllPermissions, invocation.Method);
            await service.CheckAsync(authorizationContext);
            await invocation.ProceedAsync();
        }



        internal void GetPermission(IMethodInvocation context)
        {
            var attribute = context.Method.GetAttribute<AuthorizeAttribute>() ??
                 context.TargetObject.GetAttribute<AuthorizeAttribute>();
            Permissions = attribute?.Permissions;
            RequireAllPermissions = attribute?.RequireAllPermissions ?? false;
        }
    }
}
