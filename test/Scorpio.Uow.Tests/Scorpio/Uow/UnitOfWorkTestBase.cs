namespace Scorpio.Uow
{
    public abstract class UnitOfWorkTestBase:TestBase.IntegratedTest<TestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) =>options.UseAspectCore();
    }
}
