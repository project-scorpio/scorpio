using System;
using System.Collections.Generic;
using System.Text;

using AspectCore.DependencyInjection;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.DependencyInjection.TestClasses;
using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyInjectorCallback_Tests
    {
        [Fact]
        public void Invoke_Null()
        {
            var callback = new PropertyInjectorCallback();
            Should.Throw<ArgumentNullException>(() => callback.Invoke(null, null, null));
            var resolver = Substitute.For<IServiceResolver>();
            Should.Throw<ArgumentNullException>(() => callback.Invoke(resolver, null, null));
            var serviceDefinition = new TypeServiceDefinition(typeof(PropertyInjectionService), typeof(PropertyInjectionService), Lifetime.Transient);
            Should.NotThrow(() => callback.Invoke(resolver, null, serviceDefinition)).ShouldBeNull();
        }

        [Fact]
        public void Invoke_Non()
        {
            var callback = new PropertyInjectorCallback();
            var resolver = Substitute.For<IServiceResolver>();
            var serviceDefinition = new TypeServiceDefinition(typeof(PropertyInjectionService), typeof(PropertyInjectionService), Lifetime.Transient);
            var moduleContainer = Substitute.For<IModuleContainer>();
            moduleContainer.Configure().Modules.Returns(Array.Empty<IModuleDescriptor>());
            resolver.Configure().Resolve(typeof(IModuleContainer)).Returns(moduleContainer);
            Should.NotThrow(() => callback.Invoke(resolver, moduleContainer, serviceDefinition)).ShouldBe(moduleContainer);
            var instance = new PropertyInjectionService();
            Should.NotThrow(() => callback.Invoke(resolver, instance, serviceDefinition)).ShouldBe(instance);
            var moduleDescriptor = Substitute.For<IModuleDescriptor>();
            moduleDescriptor.Configure().Assembly.Returns(typeof(Scorpio.Modularity.ScorpioModule).Assembly);
            moduleContainer.Configure().Modules.Returns(new IModuleDescriptor[] { moduleDescriptor });
            Should.NotThrow(() => callback.Invoke(resolver, instance, serviceDefinition)).ShouldBe(instance);

        }

        [Fact]
        public void Invoke()
        {
            var callback = new PropertyInjectorCallback();
            var resolver = Substitute.For<IServiceResolver>();
            var serviceDefinition = new TypeServiceDefinition(typeof(PropertyInjectionService), typeof(PropertyInjectionService), Lifetime.Transient);
            var moduleContainer = Substitute.For<IModuleContainer>();
            moduleContainer.Configure().Modules.Returns(Array.Empty<IModuleDescriptor>());
            resolver.Configure().Resolve(typeof(IModuleContainer)).Returns(moduleContainer);
            var instance = new PropertyInjectionService();
            var moduleDescriptor = Substitute.For<IModuleDescriptor>();
            moduleDescriptor.Configure().Assembly.Returns(typeof(PropertyInjectionService).Assembly);
            moduleContainer.Configure().Modules.Returns(new IModuleDescriptor[] { moduleDescriptor });
            var factory=Substitute.For<IPropertyInjectorFactory>();
            var injector=Substitute.For<IPropertyInjector>();
            factory.Configure().Create(typeof(PropertyInjectionService)).Returns(injector);
            resolver.Configure().Resolve(typeof(IPropertyInjectorFactory)).Returns(factory);
            Should.NotThrow(() => callback.Invoke(resolver, instance, serviceDefinition)).ShouldBe(instance);
            injector.Received(1).Invoke(instance);
        }
    }
}
