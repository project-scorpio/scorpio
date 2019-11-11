using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Scorpio.AspNetCore.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AspNetCoreModule))]
    public sealed class AspNetCoreMvcModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<AspNetCoreConventionalRegistrar>();
            base.PreConfigureServices(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            context.Services.Options<MvcOptions>().PreConfigure<IServiceProvider>(
                (options, serviceProvider) => options.AddScorpio(serviceProvider));
          context.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //Use DI to create controllers
            context.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            context.Services.Replace(ServiceDescriptor.Transient<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());
            //Use DI to create view components
            context.Services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

        }
    }
}
