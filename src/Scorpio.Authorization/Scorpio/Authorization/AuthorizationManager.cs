using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

namespace Scorpio.Authorization
{
    internal class AuthorizationManager : IAuthorizationManager, ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthorizationManager(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;


        public async Task AuthorizeAsync(bool requireAllPermissions, params string[] permissions)
        {
            var service = _serviceProvider.GetRequiredService<IAuthorizationService>();
            var authorizationContext = new InvocationAuthorizationContext(permissions, requireAllPermissions, null);
            await service.CheckAsync(authorizationContext);
        }
    }
}
