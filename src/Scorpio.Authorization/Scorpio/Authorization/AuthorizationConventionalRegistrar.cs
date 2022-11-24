using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Authorization.Permissions;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.Authorization
{
    internal class AuthorizationConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
           {
               config.Where(t => t.IsStandardType() && t.IsAssignableTo<IPermissionDefinitionProvider>()).AsSelf().Lifetime(ServiceLifetime.Singleton);
               config.Where(t => t.IsStandardType() && t.IsAssignableTo<IPermissionGrantingProvider>()).AsSelf().Lifetime(ServiceLifetime.Singleton);
           });
        }
    }
}
