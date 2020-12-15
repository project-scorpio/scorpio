
using System;
using System.Linq.Expressions;
using System.Reflection;

using AspectCore.Extensions.Reflection;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyInjector_Tests
    {
        [Fact]
        public void Invoke_Null()
        {
            Should.Throw<ArgumentNullException>(() => new PropertyInjector(null, null));
            Should.Throw<ArgumentNullException>(() => new PropertyInjector(Substitute.For<IServiceProvider>(), null));
            Should.Throw<ArgumentNullException>(() => new PropertyInjector(null, new PropertyResolver[0]));
        }

        [Fact]
        public void Invoke_Empty()
        {
            var injector = Should.NotThrow(() => new PropertyInjector(Substitute.For<IServiceProvider>(), new PropertyResolver[0]));
            var service = new PropertyInjectionService();
            injector.Invoke(service);
            service.PropertyService.ShouldBeNull();
        }

        [Fact]
        public void Invoke()
        {
            var service = new PropertyInjectionService();
            var property = service.Member(s => s.PropertyService) as PropertyInfo;
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.Configure().GetService(typeof(PropertyService)).Returns(new PropertyService());
            var injector = Should.NotThrow(() => new PropertyInjector(serviceProvider, new PropertyResolver[]{
                new PropertyResolver(provider => provider.GetService(property.PropertyType),property.GetReflector())
                }));
            injector.Invoke(service);
            service.PropertyService.ShouldNotBeNull();
        }
    }

}
