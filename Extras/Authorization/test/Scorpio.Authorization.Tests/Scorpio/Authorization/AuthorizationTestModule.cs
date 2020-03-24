using Microsoft.Extensions.DependencyInjection;
using Scorpio.Authorization.Permissions;
using Scorpio.Modularity;
using Scorpio.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Authorization
{
    [DependsOn(typeof(AuthorizationModule))]
    public class AuthorizationTestModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceSingleton<ICurrentPrincipalAccessor, FakePrincipalAccessor>();
            context.Services.RegisterAssemblyByConvention();
            context.Services.Configure<PermissionOptions>(opt=>
            {
                opt.DefinitionProviders.Add<FakePermissionDefinitionProvider>();
                opt.GrantingProviders.Add<FakePermissionGrantingProvider>();
            });
            base.ConfigureServices(context);
        }
    }
}
