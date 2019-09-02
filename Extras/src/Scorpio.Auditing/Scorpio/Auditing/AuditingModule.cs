using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.Modularity;
using Scorpio.Timing;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(TimingModule))]
    public sealed class AuditingModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddOptions<AuditingOptions>();
            base.PreConfigureServices(context);
        }  
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.TryAddTransient<AuditingInterceptor>();
            context.Services.RegisterAssemblyByConvention();
        }
    }
}
