using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Quartz
{
    public  class QuartzModule : ScorpioModule
    {
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionalRegistrar>();
            base.PreConfigureServices(context);
        }
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSingleton(sp => sp.GetService<ISchedulerFactory>().GetScheduler().Result);
            context.Services.AddHostedService<QuartzService>();
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            base.Initialize(context);
        }
    }
}
