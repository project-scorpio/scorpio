using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;
namespace Scorpio.Auditing
{
    public class Auditing_Tests : IntegratedTest<AuditingTestModule>
    {
        private readonly IAuditingManager _auditingManager;

        public Auditing_Tests()
        {
            _auditingManager = ServiceProvider.GetService<IAuditingManager>();
        }

        [Fact]
        public void AttributedAuditing()
        {
            using (_auditingManager.BeginScope())
            {
                var service = ServiceProvider.GetService<IAttributedAuditingInterface>();
                service.Test("test", 19);
            }
            var store = ServiceProvider.GetService<IAuditingStore>().ShouldBeOfType<FackAuditingStore>();
            store.Info.ShouldNotBeNull();
            store.Info.CurrentUser.ShouldBe("TestUser");
            var action = store.Info.Actions.ShouldHaveSingleItem();
            action.ServiceName.ShouldBe(typeof(AttributedAuditingInterface).FullName);
            action.MethodName.ShouldBe("Test");
        }

        [Fact]
        public void DisAttributedAuditing()
        {
            using (_auditingManager.BeginScope())
            {
                var service = ServiceProvider.GetService<IAttributedAuditingInterface>();
                service.Test2("test", 19);
                _auditingManager.Current.Info.ShouldNotBeNull();
            }
            var store = ServiceProvider.GetService<IAuditingStore>().ShouldBeOfType<FackAuditingStore>();
            store.Info.ShouldBeNull();
        }

        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.UseAspectCore();
        }

    }

    [Audited]
    public interface IAttributedAuditingInterface
    {
        void Test(string value, int num);

        [DisableAuditing]
        void Test2(string value, int num);
    }

    class AttributedAuditingInterface : IAttributedAuditingInterface, DependencyInjection.ITransientDependency
    {
        public void Test(string value, int num)
        {
            // Method intentionally left empty.
        }

        public void Test2(string value, int num)
        {
            // Method intentionally left empty.
        }
    }
}
