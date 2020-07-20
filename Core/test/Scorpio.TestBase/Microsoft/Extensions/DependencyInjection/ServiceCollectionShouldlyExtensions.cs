using System;
using System.Linq;

using Shouldly;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionShouldlyExtensions
    {
        public static ServiceDescriptor ShouldContainTransient(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType && s.GetImplementationType() == (implementationType ?? serviceType));

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.GetImplementationType().ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
            return serviceDescriptor;
        }

        public static ServiceDescriptor ShouldContainSingleton(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType && s.GetImplementationType() == (implementationType ?? serviceType));

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.GetImplementationType().ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
            return serviceDescriptor;
        }

        public static void ShouldContainScoped(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType && s.GetImplementationType() == (implementationType ?? serviceType));

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.GetImplementationType().ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
        }

        public static void ShouldContain(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType && s.GetImplementationType() == (implementationType ?? serviceType));

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.GetImplementationType().ShouldBe(implementationType);
            serviceDescriptor.Lifetime.ShouldBe(lifetime);
        }

        public static void ShouldNotContainService(this IServiceCollection services, Type serviceType)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldBeNull();
        }

        public static void ShouldNotContainService(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType && s.GetImplementationType() == implementationType);

            serviceDescriptor.ShouldBeNull();
        }
    }
}
