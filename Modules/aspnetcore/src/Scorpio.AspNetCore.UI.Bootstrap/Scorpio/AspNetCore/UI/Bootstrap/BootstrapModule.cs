using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AspNetCoreUiModule))]
    public class BootstrapModule: ScorpioModule
    {
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
