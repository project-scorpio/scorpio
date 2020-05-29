using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.RPC
{
    /// <summary>
    /// 
    /// </summary>
    public class RPCModule:ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
