using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DynamicProxy;

using Shouldly;

using Xunit;

namespace Scorpio.Castle.DynamicProxy
{
    public class CastleAsyncInterceptorAdapter_Tests
    {
        [Fact]
        public void InterceptAsync()
        {
            var invocation = Substitute.For<IInvocation>();
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            invocation.Configure().CaptureProceedInfo().Returns(processInfo);
            invocation.Configure().ReturnValue.Returns(Task.CompletedTask);
            var interceptor = Substitute.For<Scorpio.DynamicProxy.IInterceptor>();
            interceptor.Configure().InterceptAsync(Arg.Any<IMethodInvocation>()).ReturnsForAnyArgs(Task.CompletedTask)
                .AndDoes(async c => await c.Arg<IMethodInvocation>().ShouldBeOfType<CastleMethodInvocationAdapter>().ProceedAsync());
            IAsyncInterceptor adapter = new CastleAsyncInterceptorAdapter(interceptor);
            Should.NotThrow(() => adapter.InterceptAsynchronous(invocation));
            processInfo.Received(1).Invoke();
        }

        [Fact]
        public void InterceptAsync_T()
        {
            var invocation = Substitute.For<IInvocation>();
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            invocation.Configure().CaptureProceedInfo().Returns(processInfo);
            invocation.Configure().ReturnValue.Returns(Task.FromResult("test"));
            var interceptor = Substitute.For<Scorpio.DynamicProxy.IInterceptor>();
            interceptor.Configure().InterceptAsync(Arg.Any<IMethodInvocation>()).ReturnsForAnyArgs(Task.CompletedTask)
                .AndDoes(async c => await c.Arg<IMethodInvocation>().ShouldBeOfType<CastleMethodInvocationAdapter<string>>().ProceedAsync());
            IAsyncInterceptor adapter = new CastleAsyncInterceptorAdapter(interceptor);
            Should.NotThrow(() => adapter.InterceptAsynchronous<string>(invocation));
            processInfo.Received(1).Invoke();
        }
    }
}
