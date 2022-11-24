using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Scorpio.Authorization.Permissions;
using Scorpio.DependencyInjection;
using Scorpio.Security;
namespace Scorpio.Authorization
{
    internal class AuthorizationService : IAuthorizationService, ITransientDependency
    {
        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
        private readonly IPermissionChecker _permissionChecker;

        public AuthorizationService(ICurrentPrincipalAccessor currentPrincipalAccessor, IPermissionChecker permissionChecker)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            _permissionChecker = permissionChecker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationContext"></param>
        /// <returns></returns>
        public async Task CheckAsync(IInvocationAuthorizationContext authorizationContext)
        {

            if (authorizationContext?.Method?.AttributeExists<AllowAnonymousAttribute>() ?? false)
            {
                return;
            }
            if (!(_currentPrincipalAccessor.Principal?.Identity?.IsAuthenticated ?? false))
            {
                throw new AuthorizationException("Authorization failed! User has not logged in.");
            }
            else
            {
                await AuthorizeAsync(authorizationContext);
            }
        }

        private async Task AuthorizeAsync(IInvocationAuthorizationContext authorizationContext)
        {
            if (await IsGrantedAsync(authorizationContext))
            {
                return;
            }
            if (authorizationContext.RequireAllPermissions)
            {
                throw new AuthorizationException(
                    $"Required permissions are not granted. All of these permissions must be granted: {authorizationContext.Permissions.ExpandToString(",")}");
            }
            else
            {
                throw new AuthorizationException(
                    $"Required permissions are not granted. At least one of these permissions must be granted: {authorizationContext.Permissions.ExpandToString(",")}");
            }
        }

        private async Task<bool> IsGrantedAsync(IInvocationAuthorizationContext authorizationContext)
        {
            if (authorizationContext.Permissions.IsNullOrEmpty())
            {
                return true;
            }
            if (authorizationContext.RequireAllPermissions)
            {
                return await authorizationContext.Permissions.AllAsync(p => _permissionChecker.CheckAsync(p));
            }
            else
            {
                return await authorizationContext.Permissions.AnyAsync(p => _permissionChecker.CheckAsync(p));
            }
        }

    }
}
