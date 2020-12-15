using System;
using System.Threading.Tasks;

using NSubstitute;
using NSubstitute.Extensions;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class IMethodInvocationRuntimeExtensions_Tests
    {
        [Fact]
        public void IsAsync()
        {
            IMethodInvocation methodInvocation = null;
            Should.Throw<ArgumentNullException>(() => methodInvocation.IsAsync());
            methodInvocation = Substitute.For<IMethodInvocation>();
            methodInvocation.Configure().Method.Returns(((Func<Task<int>>)(() => Task.FromResult(1))).Method);
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeTrue();
            methodInvocation.Configure().Method.Returns(((Func<int>)(() => 1)).Method);
            methodInvocation.Configure().ReturnValue.Returns(Task.FromResult(1));
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeTrue();
            methodInvocation.Configure().ReturnValue.Returns(1);
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeFalse();
            methodInvocation.Configure().ReturnValue.Returns(Task.CompletedTask);
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeTrue();
            methodInvocation.Configure().ReturnValue.Returns(new ValueTask<int>(1));
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeTrue();
            methodInvocation.Configure().ReturnValue.Returns(new ValueTask());
            Should.NotThrow(() => methodInvocation.IsAsync()).ShouldBeTrue();

        }


    }
}
