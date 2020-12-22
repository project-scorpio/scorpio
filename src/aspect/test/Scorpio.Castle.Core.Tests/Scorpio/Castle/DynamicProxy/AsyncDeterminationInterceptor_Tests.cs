using System;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DynamicProxy;

using Shouldly;

using Xunit;

namespace Scorpio.Castle.DynamicProxy
{
    public class AsyncDeterminationInterceptor_Tests
    {
        [Fact]
        public void ProcessTask()
        {
            Func<Task> action = () => Task.CompletedTask;
            var invocation = Substitute.For<IInvocation>();
            invocation.Configure().Arguments.Returns(Array.Empty<object>());
            invocation.Configure().GenericArguments.Returns(Array.Empty<Type>());
            invocation.Configure().MethodInvocationTarget.Returns(action.Method);
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            invocation.Configure().CaptureProceedInfo().Returns(processInfo);
            invocation.Configure().Method.Returns(action.Method);
            invocation.Configure().ReturnValue.Returns(Task.CompletedTask);
            var interceptor = Substitute.For<Scorpio.DynamicProxy.IInterceptor>();
            interceptor.Configure().InterceptAsync(Arg.Any<IMethodInvocation>()).ReturnsForAnyArgs(Task.CompletedTask)
                .AndDoes(async c => await c.Arg<IMethodInvocation>().ShouldBeOfType<CastleMethodInvocationAdapter>().Action(async a =>
                {
                    await a.ProceedAsync();
                    a.Arguments.ShouldBe(invocation.Arguments);
                    a.ArgumentsDictionary.ShouldBeEmpty();
                    a.GenericArguments.ShouldBe(invocation.GenericArguments);
                    a.TargetObject.ShouldBe(invocation.MethodInvocationTarget);
                }));
            var adapter = new AsyncDeterminationInterceptor<Scorpio.DynamicProxy.IInterceptor>(interceptor);
            Should.NotThrow(() => adapter.Intercept(invocation));
            processInfo.Received(1).Invoke();
        }

        [Fact]
        public void ProcessFunc()
        {
            Func<string, Task> action = str => Task.CompletedTask;
            var invocation = Substitute.For<IInvocation>();
            var args = new string[] { string.Empty };
            invocation.Configure().Arguments.Returns(args);
            invocation.Configure().GenericArguments.Returns(new Type[] { typeof(string) });
            invocation.Configure().MethodInvocationTarget.Returns(action.Method);
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            invocation.Configure().CaptureProceedInfo().Returns(processInfo);
            invocation.Configure().Method.Returns(action.Method);
            invocation.Configure().ReturnValue.Returns(Task.CompletedTask);
            var interceptor = Substitute.For<Scorpio.DynamicProxy.IInterceptor>();
            interceptor.Configure().InterceptAsync(Arg.Any<IMethodInvocation>()).ReturnsForAnyArgs(Task.CompletedTask)
                .AndDoes(async c => await c.Arg<IMethodInvocation>().ShouldBeOfType<CastleMethodInvocationAdapter>().Action(async a =>
                {
                    await a.ProceedAsync();
                    a.Arguments.ShouldBe(invocation.Arguments);
                    a.ArgumentsDictionary.ShouldHaveSingleItem().Value.ShouldBe(string.Empty);
                    a.GenericArguments.ShouldBe(invocation.GenericArguments);
                    a.TargetObject.ShouldBe(invocation.MethodInvocationTarget);
                }));
            var adapter = new AsyncDeterminationInterceptor<Scorpio.DynamicProxy.IInterceptor>(interceptor);
            Should.NotThrow(() => adapter.Intercept(invocation));
            processInfo.Received(1).Invoke();
        }
    }
}
