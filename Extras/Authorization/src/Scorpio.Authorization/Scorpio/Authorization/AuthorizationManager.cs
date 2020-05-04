using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    class AuthorizationManager : IAuthorizationManager, ISingletonDependency
    {
        private IServiceProvider _serviceProvider;

        public AuthorizationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async Task AuthorizeAsync(bool requireAllPermissions, params string[] permissions)
        {
            var service = _serviceProvider.GetRequiredService<IAuthorizationService>();
            var authorizationContext = new InvocationAuthorizationContext(permissions, requireAllPermissions, null);
            await service.CheckAsync(authorizationContext);
        }
    }
}
