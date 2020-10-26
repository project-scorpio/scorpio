using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.AspNetCore.Auditing;
using Scorpio.AspNetCore.Authorization;
using Scorpio.Auditing;
using Scorpio.Authorization;
using Scorpio.Modularity;
using Scorpio.Uow;

namespace Scorpio.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(AuditingModule))]
    [DependsOn(typeof(AuthorizationModule))]
    public sealed class AspNetCoreModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Options<AuditingOptions>().Configure<IServiceProvider>((options, serviceProvider) =>
             {
                 options.Contributors.Add(new AspNetCoreAuditContributor(serviceProvider));
             });
            context.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.AddAuthorization();

            context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Initialize(ApplicationInitializationContext context)
        {
            base.Initialize(context);
        }
    }
}
