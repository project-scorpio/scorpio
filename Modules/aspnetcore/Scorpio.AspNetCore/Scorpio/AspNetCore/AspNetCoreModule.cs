using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Security;
using Scorpio.Uow;
using Scorpio.Threading;
using Scorpio.Auditing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Scorpio.AspNetCore.Auditing;
using Scorpio.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Scorpio.Authorization;

namespace Scorpio.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(ThreadingModule))]
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
           context.Services.Configure<AuditingOptions>(options =>
            {
                options.Contributors.Add(new AspNetCoreAuditContributor());
            });
            context.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.AddAuthorization();

            context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
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
