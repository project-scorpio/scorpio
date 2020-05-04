using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Data;
using Scorpio.Uow;

namespace Scorpio.Application
{
    [DependsOn(typeof(DataModule))]
    [DependsOn(typeof(UnitOfWorkModule))]
    public class ApplicationModule: ScorpioModule
    {
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionaInterceptorRegistrar>();
            base.PreConfigureServices(context);
        }
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
