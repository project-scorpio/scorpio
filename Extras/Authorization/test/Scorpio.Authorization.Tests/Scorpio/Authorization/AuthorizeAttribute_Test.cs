using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization
{
    public class AuthorizeAttribute_Test : IntegratedTest<AuthorizationTestModule>
    {

        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.UseAspectCore();
            base.SetBootstrapperCreationOptions(options);
        }

        protected override void AfterAddBootstrapper(IServiceCollection services)
        {
            services.AddTransient<IAuthorizeAttributeTestService, AuthorizeAttributeTestService>();
            base.AfterAddBootstrapper(services);
        }

        [Fact]
        public void AuthorizeService()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeByServcieAsync().ShouldThrow<AspectCore.DynamicProxy.AspectInvocationException>();
        }

        [Fact]
        public void AuthorizeNotPermission()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeByAttributeAsync().ShouldNotThrow();
        }
        [Fact]
        public void AuthorizeAnonymous()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeAnonymousAsync().ShouldNotThrow();
        }

    }
}
