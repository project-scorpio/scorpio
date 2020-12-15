using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DynamicProxy;
using Scorpio.Castle.DynamicProxy;
using Shouldly;
using Xunit;
using System.Linq;
using NSubstitute.ClearExtensions;
using Scorpio.DynamicProxy.TestClasses;

namespace Scorpio.Castle
{
    public class CastleCoreModule_Tests : CastleCoreTestBase
    {
        protected override Bootstrapper CreateBootstrapper(IServiceCollection services)
        {
            services.AddSingleton(Substitute.For<IInterceptor>());
            return base.CreateBootstrapper(services);
        }

        [Fact]
        public void Process()
        {
            var interceptor = GetRequiredService<IInterceptor>();
            interceptor.ClearSubstitute();
            interceptor.Configure()
              .InterceptAsync(Arg.Any<IMethodInvocation>())
              .ReturnsForAnyArgs(Task.CompletedTask)
              .AndDoes(c => c.Arg<IMethodInvocation>().Action(i =>
              {
                  i.ReturnValue.ShouldBeNull();
                  i.Arguments.SequenceEqual(new object[] { default(int), string.Empty }).ShouldBeTrue();
                  i.Method.Name.ShouldBe(nameof(IProxiedService.InterfaceMethod));
              }));
            var service = ServiceProvider.GetServiceWithInterfaceProxy<IProxiedService, IInterceptor>();
            Should.NotThrow(() => service.InterfaceMethod(default, string.Empty));
            interceptor.ReceivedWithAnyArgs(1).InterceptAsync(Arg.Any<IMethodInvocation>());
        }

        [Fact]
        public void ProcessClass()
        {
            var interceptor = GetRequiredService<IInterceptor>();
            interceptor.ClearSubstitute();
            interceptor.Configure()
              .InterceptAsync(Arg.Any<IMethodInvocation>())
              .ReturnsForAnyArgs(Task.CompletedTask)
              .AndDoes(c => c.Arg<IMethodInvocation>().Action(i =>
              {
                  i.ReturnValue.ShouldBeNull();
                  i.Arguments.SequenceEqual(new object[] { default(int), string.Empty }).ShouldBeTrue();
                  i.Method.Name.ShouldBe(nameof(TestProxiedService.ProxiedMethod));
              }));
            var service = ServiceProvider.GetServiceWithClassProxy<TestProxiedService, IInterceptor>();
            Should.NotThrow(() => service.ProxiedMethod(default, string.Empty));
            interceptor.ReceivedWithAnyArgs(1).InterceptAsync(Arg.Any<IMethodInvocation>());
        }

         [Fact]
        public void ProcessNonProxy()
        {
            var interceptor = GetRequiredService<IInterceptor>();
            interceptor.ClearSubstitute();
            interceptor.Configure()
              .InterceptAsync(Arg.Any<IMethodInvocation>())
              .ReturnsForAnyArgs(Task.CompletedTask);
            var service = ServiceProvider.GetServiceWithClassProxy<TestProxiedService, IInterceptor>();
            Should.NotThrow(() => service.NonProxiedMethod(default, string.Empty));
            interceptor.ReceivedWithAnyArgs(0).InterceptAsync(Arg.Any<IMethodInvocation>());
        }
    }
}
