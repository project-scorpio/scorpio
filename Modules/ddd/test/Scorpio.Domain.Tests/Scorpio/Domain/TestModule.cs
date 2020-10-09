
using Scorpio.Modularity;

namespace Scorpio.Domain
{
    [DependsOn(typeof(DomainModule))]
    public class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.RegisterAssemblyByConvention();
        }
    }
}
