using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Scorpio.AspNetCore.UI
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AspNetCoreModule))]
    public sealed class AspNetCoreUiModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<AspNetCoreUiConventionalRegistrar>();
            base.PreConfigureServices(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }
    }
}
