
using Scorpio.Modularity;

namespace Scorpio.Application
{
    [DependsOn(typeof(ApplicationModule))]
    public class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.RegisterAssemblyByConvention();
        }
    }
}
