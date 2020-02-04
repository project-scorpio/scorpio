using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Sample.AspnetCore
{
    public class SampleModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            Console.WriteLine($"Module {nameof(SampleModule)} is initialized.");
        }

        public override void Shutdown(ApplicationShutdownContext context)
        {
            Console.WriteLine($"Module {nameof(SampleModule)} is shutdown.");
        }
    }
}
