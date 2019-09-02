using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TimingModule:ScorpioModule
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
