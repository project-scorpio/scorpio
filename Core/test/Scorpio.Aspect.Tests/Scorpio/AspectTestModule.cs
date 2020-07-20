using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DynamicProxy;
using Scorpio.Modularity;
using Scorpio.TestBase;

namespace Scorpio
{
    [DependsOn(typeof(TestBaseModule))]
    public class AspectTestModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.AddConventionalRegistrar<TestConventionalInterceptorRegistrar>();
            context.RegisterAssemblyByConvention();
           context.Services.AddTransient<IInterceptorTestService, InterceptorTestService>();
            context.Services.AddTransient<INonInterceptorTestService, NonInterceptorTestService>();

            base.ConfigureServices(context);
        }
    }
}
