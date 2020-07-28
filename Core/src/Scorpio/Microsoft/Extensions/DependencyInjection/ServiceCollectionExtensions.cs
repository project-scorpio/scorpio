using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Scorpio;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection DoConventionalAction<TAction>(
            this IServiceCollection services,
            IEnumerable<Type> types,
            Action<IConventionalConfiguration<TAction>> configureAction)
            where TAction : ConventionalActionBase
        {
            var config = new ConventionalConfiguration<TAction>(services, types);
            configureAction(config);
            var action = Activator.CreateInstance(
                typeof(TAction),
                BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                null,
                new object[] { config },
                null) as TAction;
            action.Action();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterConventionalDependencyInject(
            this IServiceCollection services,
            IEnumerable<Type> types,
            Action<IConventionalConfiguration<ConventionalDependencyAction>> configureAction)
        {
            return services.DoConventionalAction(types, configureAction);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrAdd<T>(this IServiceCollection services, Func<IServiceCollection, T> func) where T : class
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                service = func(services);
                services.AddSingleton(service);
            }
            return service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrAdd<T>(this IServiceCollection services, T instance) where T : class
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                service = instance;
                services.AddSingleton(service);
            }
            return service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddSingleton<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services, TService instance)
            where TService : class
        {
            RemoveService<TService>(services);
            return services.AddSingleton(instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddTransient<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddScoped<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        public static IServiceCollection RemoveService<TService>(IServiceCollection services) where TService : class
        {
            var old = services.FirstOrDefault(s => s.ServiceType == typeof(TService));
            if (old != null)
            {
                services.Remove(old);
            }
            return services;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceDescriptor"></param>
        /// <param name="replaceAll"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceOrAdd(this IServiceCollection services, ServiceDescriptor serviceDescriptor, bool replaceAll = false)
        {

            Check.NotNull(services, nameof(services));
            Check.NotNull(serviceDescriptor, nameof(serviceDescriptor));

            if (!replaceAll)
            {
                var implementationType = serviceDescriptor.GetImplementationType();

                services.RemoveAll(s => s.ServiceType == serviceDescriptor.ServiceType && s.GetImplementationType() == implementationType);

            }
            else
            {
                services.RemoveAll(s => s.ServiceType == serviceDescriptor.ServiceType);
            }
            services.Add(serviceDescriptor);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TSourceImplementation"></typeparam>
        /// <typeparam name="TDestinationImplementation"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceEnumerable<TService, TSourceImplementation, TDestinationImplementation>(this IServiceCollection services,ServiceLifetime lifetime= ServiceLifetime.Transient)
            where TService : class
            where TSourceImplementation : class, TService
            where TDestinationImplementation : class, TService
        {
            return services.ReplaceEnumerable( ServiceDescriptor.Transient<TService, TSourceImplementation>(), ServiceDescriptor.Describe(typeof(TService), typeof(TDestinationImplementation), lifetime));
        }

        /// <summary>
        /// Removes the first service in <see cref="IServiceCollection"/> with the same service type
        /// as <paramref name="sourcedescriptor"/> and adds <paramef name="descriptor"/> to the collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="sourcedescriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <param name="destdescriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <returns></returns>
        public static IServiceCollection ReplaceEnumerable(
            this IServiceCollection services,
            ServiceDescriptor sourcedescriptor, ServiceDescriptor destdescriptor)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(sourcedescriptor, nameof(sourcedescriptor));
            Check.NotNull(destdescriptor, nameof(destdescriptor));

            var implementationType = sourcedescriptor.GetImplementationType();

            if (implementationType == typeof(object) ||
                implementationType == sourcedescriptor.ServiceType)
            {
                throw new ArgumentException($"Implementation type cannot be '{implementationType}' because it is indistinguishable from other services registered for '{sourcedescriptor.ServiceType}'.", nameof(sourcedescriptor));
            }


            var registeredServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == sourcedescriptor.ServiceType &&
                              s.GetImplementationType() == implementationType);
            if (registeredServiceDescriptor != null)
            {
                services.Remove(registeredServiceDescriptor);
            }

            services.Add(destdescriptor);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RemoveEnumerable<TService,  TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            return services.RemoveEnumerable(ServiceDescriptor.Transient<TService, TImplementation>());
        }


        /// <summary>
        /// Removes the first service in <see cref="IServiceCollection"/> with the same service type
        /// as <paramref name="descriptor"/> and adds <paramef name="descriptor"/> to the collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <returns></returns>
        public static IServiceCollection RemoveEnumerable(
            this IServiceCollection services,
            ServiceDescriptor descriptor)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(descriptor, nameof(descriptor));

            var implementationType = descriptor.GetImplementationType();

            if (implementationType == typeof(object) ||
                implementationType == descriptor.ServiceType)
            {
                throw new ArgumentException($"Implementation type cannot be '{implementationType}' because it is indistinguishable from other services registered for '{descriptor.ServiceType}'.", nameof(descriptor));
            }


            var registeredServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == descriptor.ServiceType &&
                              s.GetImplementationType() == implementationType);
            if (registeredServiceDescriptor != null)
            {
                services.Remove(registeredServiceDescriptor);
            }
            return services;
        }

        internal static Type GetImplementationType(this ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor.ImplementationType != null)
            {
                return serviceDescriptor.ImplementationType;
            }
            else if (serviceDescriptor.ImplementationInstance != null)
            {
                return serviceDescriptor.ImplementationInstance.GetType();
            }
            else if (serviceDescriptor.ImplementationFactory != null)
            {
                var typeArguments = serviceDescriptor.ImplementationFactory.GetType().GenericTypeArguments;

                Debug.Assert(typeArguments.Length == 2);

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType, ImplementationInstance or ImplementationFactory must be non null");
            return null;
        }


    }
}
