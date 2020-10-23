using Microsoft.Extensions.DependencyInjection;

using Scorpio.Authorization.Permissions;
using Scorpio.Modularity;
using Scorpio.Security;

namespace Scorpio.Authorization
{
    [DependsOn(typeof(AuthorizationModule))]
    public class AuthorizationTestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceSingleton<ICurrentPrincipalAccessor, FakePrincipalAccessor>();
            context.Services.Configure<PermissionOptions>(opt =>
            {
                opt.DefinitionProviders.Add<FakePermissionDefinitionProvider>();
                opt.GrantingProviders.Add<FakePermissionGrantingProvider>();
            });
        }
    }
}
