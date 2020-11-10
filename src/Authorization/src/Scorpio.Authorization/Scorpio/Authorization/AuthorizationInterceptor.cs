using System.Reflection;
using System.Threading.Tasks;

using AspectCore.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Authorization
{
    /// <summary>
    /// Authorization interceptor.
    /// </summary>
    internal class AuthorizationInterceptor : AbstractInterceptor
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
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (Aspects.CrossCuttingConcerns.IsApplied(context.Implementation, Concern))
            {
                await next(context);
                return;
            }
            GetPermission(context);
            var service = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var authorizationContext = new InvocationAuthorizationContext(Permissions, RequireAllPermissions, context.ServiceMethod);
            await service.CheckAsync(authorizationContext);
            await next(context);
        }

        internal void GetPermission(AspectContext context)
        {
            var attribute = context.ImplementationMethod.GetAttribute<AuthorizeAttribute>() ??
                 context.ServiceMethod.GetAttribute<AuthorizeAttribute>() ??
                 context.Implementation.GetAttribute<AuthorizeAttribute>() ??
                 context.ServiceMethod.DeclaringType.GetAttribute<AuthorizeAttribute>();
            Permissions = attribute?.Permissions;
            RequireAllPermissions = attribute?.RequireAllPermissions ?? false;
        }
    }
}
