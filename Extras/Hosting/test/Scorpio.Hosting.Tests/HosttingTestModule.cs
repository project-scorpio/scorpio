using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;
namespace Scorpio.Hosting.Tests
{
    public class HosttingTestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }
    }
}
