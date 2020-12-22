using System;
using System.Threading.Tasks;

using AspectCore.DynamicProxy;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class AspectCoreInterceptorAdapter_Tests
    {
        [Fact]
        public void Ctor()
        {
            Should.Throw<ArgumentNullException>(() => new AspectCoreInterceptorAdapter<IInterceptor>(null));
            Should.NotThrow(() => new AspectCoreInterceptorAdapter<IInterceptor>(Substitute.For<IInterceptor>()));
        }

        [Fact]
        public void Invoke()
        {
            var interceptor = Substitute.For<IInterceptor>();
            _ = interceptor.Configure()
                .InterceptAsync(Arg.Any<IMethodInvocation>())
                .Returns(Task.CompletedTask)
                .AndDoes(c => c.Arg<IMethodInvocation>()
                     .ShouldBeOfType<AspectCoreMethodInvocation>()
                     .ShouldNotBeNull());
            var context=Substitute.ForPartsOf<AspectContext>();
            var func=Substitute.For<AspectDelegate>();
            var adapter = Should.NotThrow(() => new AspectCoreInterceptorAdapter<IInterceptor>(interceptor));
            Should.Throw<ArgumentNullException>(() => adapter.Invoke(null, null));
            Should.Throw<ArgumentNullException>(() => adapter.Invoke(context, null));
            Should.Throw<ArgumentNullException>(() => adapter.Invoke(null, func));
            Should.NotThrow(() => adapter.Invoke(context, func));
            interceptor.Received(1).InterceptAsync(Arg.Any<IMethodInvocation>());
        }
    }
}
