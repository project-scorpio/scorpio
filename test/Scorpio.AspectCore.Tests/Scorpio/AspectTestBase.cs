using Scorpio.TestBase;

namespace Scorpio
{
    public abstract class AspectTestBase : IntegratedTest<AspectTestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();
    }
}
