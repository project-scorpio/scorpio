using Scorpio.TestBase;

namespace Scorpio
{
    public abstract class AspectIntegratedTest : IntegratedTest<AspectTestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();
    }
}
