
using Scorpio.Modularity;
using Scorpio.TestBase;

namespace Scorpio
{
    public abstract class AspectIntegratedTest<TStartupModule> : IntegratedTest<TStartupModule>
        where TStartupModule : IScorpioModule
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();
    }
}
