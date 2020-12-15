using System;
using System.Collections.Generic;
using System.Text;

using NSubstitute;

using Scorpio.DependencyInjection.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyInjectorFactory_Tests
    {

        [Fact]
        public void Create()
        {
            Should.Throw<ArgumentNullException>(() => new PropertyInjectorFactory(null));
            var serviceProvider=Substitute.For<IServiceProvider>();
            var factory = Should.NotThrow(() => new PropertyInjectorFactory(serviceProvider));
            factory.Create(typeof(PropertyInjectionService)).ShouldBeOfType<PropertyInjector>().ShouldNotBeNull();

        }
    }
}
