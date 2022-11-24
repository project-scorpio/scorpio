using System;
using System.Collections.Generic;

using AspectCore.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.DependencyInjection.TestClasses;
using Scorpio.DynamicProxy;
using Scorpio.DynamicProxy.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class ServiceDefinitionExtensions_Tests
    {
        [Fact]
        public void RequiredPropertyInjection_Proxy_Interface()
        {
            var proxDefinitionType = typeof(AspectCore.DependencyInjection.PropertyResolver).Assembly.GetType("AspectCore.DependencyInjection.ProxyServiceDefinition");
            var serviceDefinition = new TypeServiceDefinition(typeof(IServiceProvider), typeof(ServiceProvider), Lifetime.Transient);
            var proxDefinition = Activator.CreateInstance(proxDefinitionType, serviceDefinition, typeof(AspectCoreInterceptorAdapter<TestInterceptor>)) as ServiceDefinition;
            proxDefinition.RequiredPropertyInjection().ShouldBeFalse();
        }

        [Fact]
        public void RequiredPropertyInjection_NonImplemenation()
        {
            var serviceDefinition = Substitute.ForPartsOf<ServiceDefinition>(typeof(IServiceProvider), Lifetime.Transient);
            serviceDefinition.RequiredPropertyInjection().ShouldBeFalse();
        }

        [Fact]
        public void RequiredPropertyInjection_Object()
        {
            var serviceDefinition = new TypeServiceDefinition(typeof(IServiceProvider), typeof(object), Lifetime.Transient);
            serviceDefinition.RequiredPropertyInjection().ShouldBeTrue();
        }

        [Fact]
        public void RequiredPropertyInjection_Non()
        {
            var serviceDefinition = new TypeServiceDefinition(typeof(IServiceProvider), typeof(NonPropertyInjectionService), Lifetime.Transient);
            serviceDefinition.RequiredPropertyInjection().ShouldBeFalse();
        }

        [Fact]
        public void RequiredPropertyInjection()
        {
            var serviceDefinition = new TypeServiceDefinition(typeof(IServiceProvider), typeof(PropertyInjectionService), Lifetime.Transient);
            serviceDefinition.RequiredPropertyInjection().ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(GetDatas))]
        public void GetImplementationType(ServiceDefinition definition, Type type) => definition.GetImplementationType().ShouldBe(type);

        public static IEnumerable<object[]> GetDatas()
        {
            yield return new object[]
            {
                new TypeServiceDefinition(typeof(IServiceProvider),typeof(ServiceProvider), Lifetime.Transient),typeof(ServiceProvider)
            };
            yield return new object[]
            {
                new DelegateServiceDefinition(typeof(IServiceProvider),s=>new ServiceCollection().BuildServiceProvider(), Lifetime.Transient),typeof(object)
            };
            yield return new object[]
            {
                new InstanceServiceDefinition(typeof(IServiceProvider),new ServiceCollection().BuildServiceProvider()),typeof(ServiceProvider)
            };
            var proxDefinitionType = typeof(AspectCore.DependencyInjection.PropertyResolver).Assembly.GetType("AspectCore.DependencyInjection.ProxyServiceDefinition");
            var serviceDefinition = new TypeServiceDefinition(typeof(IServiceProvider), typeof(ServiceProvider), Lifetime.Transient);
            var proxDefinition = Activator.CreateInstance(proxDefinitionType, serviceDefinition, typeof(AspectCoreInterceptorAdapter<TestInterceptor>)) as ServiceDefinition;
            yield return new object[]
            {
                proxDefinition,typeof(AspectCoreInterceptorAdapter<TestInterceptor>)
            };
        }
    }
}
