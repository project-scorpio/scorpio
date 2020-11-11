namespace Scorpio.EntityFrameworkCore
{
    public abstract class EntityFrameworkCoreTestBase : TestBase.IntegratedTest<EntityFrameworkCoreTestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            base.SetBootstrapperCreationOptions(options);
            options.UseAspectCore();
        }
    }
}
