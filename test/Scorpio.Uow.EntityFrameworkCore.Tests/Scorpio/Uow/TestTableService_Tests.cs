
using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Uow
{
    public class TestTableService_Tests : IntegratedTest<TestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();

        [Fact]
        public void Get()
        {
            var service = ServiceProvider.GetService<ITestTableService>();

            Should.NotThrow(() => service.Get(6));
        }

        [Fact]
        public void Add()
        {
            var service = ServiceProvider.GetService<ITestTableService>();

            Should.NotThrow(() => service.Add(new TestTable { Id = 7, StringValue = "Tom7" }));
        }
        [Fact]
        public void AddAsync()
        {
            var service = ServiceProvider.GetService<ITestTableService>();

            Should.NotThrow(() => service.AddAsync(new TestTable { Id = 7, StringValue = "Tom7" }));
        }
    }
}
