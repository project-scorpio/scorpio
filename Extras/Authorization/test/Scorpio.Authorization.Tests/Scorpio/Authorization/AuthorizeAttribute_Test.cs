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
            service.AuthorizeByServcieAsync().ShouldThrow<AspectCore.DynamicProxy.AspectInvocationException>().InnerException.ShouldBeOfType<AuthorizationException>();
        }

        [Fact]
        public void AuthorizeByNotAllAttributeAsync()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeByNotAllAttributeAsync().ShouldThrow<AspectCore.DynamicProxy.AspectInvocationException>().InnerException.ShouldBeOfType<AuthorizationException>();
        }

        [Fact]
        public void AuthorizeByApplyAttributeAsync()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeByNotAllAttributeAsync().ShouldThrow<AspectCore.DynamicProxy.AspectInvocationException>().InnerException.ShouldBeOfType<AuthorizationException>();
            using (Aspects.CrossCuttingConcerns.Applying(service, AuthorizationInterceptor.Concern))
            {
                service.AuthorizeByAllAttributeAsync().ShouldNotThrow();
            }
            service.AuthorizeByNotAllAttributeAsync().ShouldThrow<AspectCore.DynamicProxy.AspectInvocationException>().InnerException.ShouldBeOfType<AuthorizationException>();
        }

        [Fact]
        public void AuthorizeByAllAttributeAsync()
        {
            var service = ServiceProvider.GetService<IAuthorizeAttributeTestService>();
            service.AuthorizeByAllAttributeAsync().ShouldNotThrow();
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
