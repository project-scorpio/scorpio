using System;

using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class AspectCoreMethodInvocation_Tests
    {
        [Fact]
        public void AspectCoreMethodInvocation()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            var func = Substitute.For<AspectDelegate>();
            Should.Throw<ArgumentNullException>(() => new AspectCoreMethodInvocation(null, null));
            Should.Throw<ArgumentNullException>(() => new AspectCoreMethodInvocation(context, null));
            Should.Throw<ArgumentNullException>(() => new AspectCoreMethodInvocation(null, func));
            Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
        }

        [Fact]
        public void Arguments()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            context.Configure().Parameters.Returns(new object[0]);
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.Arguments.ShouldBeEmpty();
            context.Configure().Parameters.Returns(new object[] { "Test" });
            invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.Arguments.ShouldHaveSingleItem().ShouldBeOfType<string>().ShouldBe("Test");
        }

        [Fact]
        public void ArgumentsDictionary()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            context.Configure().Parameters.Returns(new object[0]);
            context.Configure().ImplementationMethod.Returns(typeof(object).GetMethod(nameof(object.ToString)));
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.ArgumentsDictionary.ShouldBeEmpty();
            context.Configure().Parameters.Returns(new object[] { typeof(PropertyService) });
            context.Configure().ImplementationMethod.Returns(typeof(IServiceProvider).GetMethod(nameof(IServiceProvider.GetService)));
            invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.ArgumentsDictionary.ShouldHaveSingleItem().Key.ShouldBe("serviceType");
        }

        [Fact]
        public void GenericArguments()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            context.Configure().ImplementationMethod.Returns(typeof(object).GetMethod(nameof(object.ToString)));
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.GenericArguments.ShouldBeEmpty();
            context.Configure().ImplementationMethod.Returns(typeof(ServiceResolverExtensions).GetMethod(nameof(ServiceResolverExtensions.Resolve)).MakeGenericMethod(typeof(IServiceProvider)));
            invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.GenericArguments.ShouldHaveSingleItem().ShouldBe(typeof(IServiceProvider));
        }

        [Fact]
        public void ReturnValues()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            context.Configure().ReturnValue.Returns(null);
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.ReturnValue.ShouldBeNull();
            context.Configure().ReturnValue.Returns("Test");
            invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.ReturnValue.ShouldBe("Test");
            context.Configure().ReturnValue = Arg.Do<object>(o => o.ShouldBeOfType<string>().ShouldBe("Hello"));
            invocation.ReturnValue = "Hello";
            context.Received(1).ReturnValue = "Hello";
        }

        [Fact]
        public void TargetObject()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            context.Configure().Implementation.Returns(null);
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.TargetObject.ShouldBeNull();
            context.Configure().Implementation.Returns("Test");
            invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            invocation.TargetObject.ShouldBe("Test");
        }

        [Fact]
        public void ProceedAsync()
        {
            var context = Substitute.ForPartsOf<AspectContext>();
            var func = Substitute.For<AspectDelegate>();
            var invocation = Should.NotThrow(() => new AspectCoreMethodInvocation(context, func));
            Should.NotThrow(() => invocation.ProceedAsync());
            context.Received(1).Invoke(func);
        }

    }
}
