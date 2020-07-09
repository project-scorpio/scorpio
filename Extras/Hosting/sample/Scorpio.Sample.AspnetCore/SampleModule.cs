using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;
namespace Scorpio.Sample.AspnetCore
{
    public class SampleModule : ScorpioModule
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
