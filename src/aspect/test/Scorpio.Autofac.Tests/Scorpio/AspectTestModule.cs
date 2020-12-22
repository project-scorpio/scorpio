

using Scorpio.DynamicProxy;
using Scorpio.Modularity;

namespace Scorpio
{
    [DependsOn(typeof(AspectTestBaseModule))]
    public class AspectTestModule : ScorpioModule
    {
        public override void PreConfigureServices(ConfigureServicesContext context) => context.AddConventionalRegistrar<TestConventionalInterceptorRegistrar>();
       
    }
}
