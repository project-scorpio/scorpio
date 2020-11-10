using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Application.Services;
using Scorpio.DependencyInjection;
using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Application
{
    public class Module_Tests : IntegratedTest<TestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();
        [Fact]
        public void GetAppService() => ServiceProvider.GetRequiredService<ITestAppService>().ShouldBeOfType<TestAppService>();
    }

    public interface ITestAppService : IApplicationService
    {

    }

    internal class TestAppService : ApplicationService, ITestAppService, ITransientDependency
    {
        public TestAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
