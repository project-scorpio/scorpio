using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;
using Scorpio.Domain.Services;
using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Domain
{
    public class Module_Tests : IntegratedTest<TestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options) => options.UseAspectCore();
        [Fact]
        public void GetAppService() => ServiceProvider.GetRequiredService<ITestAppService>().ShouldBeOfType<TestAppService>();
    }

    public interface ITestAppService : IDomainService
    {

    }

    internal class TestAppService : DomainService, ITestAppService, ITransientDependency
    {
        public TestAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
