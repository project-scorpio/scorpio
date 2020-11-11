
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio
{
    public class AspectTestModule : ScorpioModule
    {
        public override void PreConfigureServices(ConfigureServicesContext context) => context.AddConventionalRegistrar<TestConventionalInterceptorRegistrar>();
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddTransient<IInterceptorTestService, InterceptorTestService>();
            context.Services.AddTransient<INonInterceptorTestService, NonInterceptorTestService>();
        }
    }
}
