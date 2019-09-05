using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.Authorization.Permissions;
using Scorpio.Security;

namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AuthorizationModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<AuthorizationConventionalRegistrar>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddOptions<PermissionOptions>();
            context.Services.TryAddTransient<AuthorizationInterceptor>();
            context.Services.RegisterAssemblyByConvention();
        }
    }
}
