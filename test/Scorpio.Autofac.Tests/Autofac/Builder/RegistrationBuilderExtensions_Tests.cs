using System;
using System.Collections.Generic;
using System.Linq;

using Autofac.Core;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio;
using Scorpio.Castle.DynamicProxy;
using Scorpio.DependencyInjection.TestClasses;
using Scorpio.DynamicProxy.TestClasses;
using Scorpio.Modularity;

using Shouldly;

using Xunit;

namespace Autofac.Builder
{
    public class RegistrationBuilderExtensions_Tests
    {
        [Fact]
        public void ConfigureConventions()
        {
            var build = new ContainerBuilder().RegisterType<TestProxiedService>().As<IProxiedService>();
            var interceptors = new ServiceInterceptorList();
            var container = Substitute.For<IModuleContainer>();
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(null, null));
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(null, interceptors));
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(container, null));
            Should.NotThrow(() => build.ConfigureConventions(container, interceptors));
            build = null;
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(null, null));
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(null, interceptors));
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(container, null));
            Should.Throw<ArgumentNullException>(() => build.ConfigureConventions(container, interceptors));
        }

        [Fact]
        public void InvokeRegistrationActions()
        {
            var build = new ContainerBuilder().RegisterType<TestProxiedService>();
            var interceptors = new ServiceInterceptorList();
            interceptors.Add(typeof(TestProxiedService), typeof(TestInterceptor));
            var container = Substitute.For<IModuleContainer>();
            Should.NotThrow(() => build.ConfigureConventions(container, interceptors));
            build.RegistrationData.Metadata.Where(kv => kv.Key == "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName").ShouldHaveSingleItem().Action(kv =>
                {
                    kv.Key.ShouldBe("Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName");
                    kv.Value.As<IEnumerable<Service>>().ShouldHaveSingleItem().ShouldBeOfType<TypedService>().ServiceType.ShouldBe(typeof(AsyncDeterminationInterceptor<TestInterceptor>));
                }
            );
            build = new ContainerBuilder().RegisterType<TestProxiedService>().As<IProxiedService>();
            Should.NotThrow(() => build.ConfigureConventions(container, interceptors));
            build.RegistrationData.Metadata.Where(kv => kv.Key == "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName").ShouldHaveSingleItem().Action(kv =>
                {
                    kv.Key.ShouldBe("Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName");
                    kv.Value.As<IEnumerable<Service>>().ShouldHaveSingleItem().ShouldBeOfType<TypedService>().ServiceType.ShouldBe(typeof(AsyncDeterminationInterceptor<TestInterceptor>));
                }
            );

        }

        [Fact]
        public void EnablePropertyInjection()
        {
            var build = new ContainerBuilder().RegisterType<PropertyInjectionService>();
            var interceptors = new ServiceInterceptorList();
            interceptors.Add(typeof(TestProxiedService), typeof(TestInterceptor));
            var container = Substitute.For<IModuleContainer>();
            var module = Substitute.For<IModuleDescriptor>();
            module.Configure().Assembly.Returns(typeof(PropertyInjectionService).Assembly);
            container.Configure().Modules.Returns(new List<IModuleDescriptor> { module });
            Should.NotThrow(() => build.ConfigureConventions(container, interceptors));
            build.RegistrationData.Services.ShouldHaveSingleItem();
        }
    }
}
