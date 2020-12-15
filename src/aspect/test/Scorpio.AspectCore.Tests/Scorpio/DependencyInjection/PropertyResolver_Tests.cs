using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using AspectCore.Extensions.Reflection;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyResolver_Tests
    {
        [Fact]
        public void Ctor()
        {
            var func = Substitute.For<Func<IServiceProvider, object>>();
            var reflector = (new PropertyInjectionService().Member(p => p.PropertyService) as PropertyInfo).GetReflector();
            Should.Throw<ArgumentNullException>(() => new PropertyResolver(null, null));
            Should.Throw<ArgumentNullException>(() => new PropertyResolver(func, null));
            Should.Throw<ArgumentNullException>(() => new PropertyResolver(null, reflector));
            Should.NotThrow(() => new PropertyResolver(func, reflector));
        }

        [Fact]
        public void Resolve()
        {
            var instance = new PropertyInjectionService();
            var func = Substitute.For<Func<IServiceProvider, object>>();
            var reflector = (instance.Member(p => p.PropertyService) as PropertyInfo).GetReflector();
            var resolver = Should.NotThrow(() => new PropertyResolver(func, reflector));
            var serviceProvider=Substitute.For<IServiceProvider>();
            var ps=new PropertyService();
            func.Configure().Invoke(serviceProvider).Returns(ps);
            Should.Throw<ArgumentNullException>(() => resolver.Resolve(null, null));
            Should.Throw<ArgumentNullException>(() => resolver.Resolve(serviceProvider, null));
            Should.Throw<ArgumentNullException>(() => resolver.Resolve(null, instance));
            Should.NotThrow(() => resolver.Resolve(serviceProvider, instance));
            instance.PropertyService.ShouldBe(ps);
        }
    }
}
