using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using Scorpio.Options;

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
        public static IServiceCollection RegisterAssembly(this IServiceCollection services, IEnumerable<Type> types, Action<IConventionalConfiguration> configureAction)
        {
            return DoConventionalAction<ConventionalDependencyAction>(services, types, configureAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection DoConventionalAction<TAction>(this IServiceCollection services, IEnumerable<Type> types, Action<IConventionalConfiguration> configureAction) where TAction : ConventionalActionBase
        {
            var config = new ConventionalConfiguration(services);
            configureAction(config);
            var action = Activator.CreateInstance(typeof(TAction), config, types) as TAction;
            action.Action();
            return services;
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
        /// <returns></returns>
        public static IServiceCollection ReplaceEnumerable(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
        {
            return services.ReplaceEnumerable(ServiceDescriptor.Transient(serviceDescriptor.ServiceType, serviceDescriptor.GetImplementationType()), serviceDescriptor);
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
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceDescriptor == null)
            {
                throw new ArgumentNullException(nameof(serviceDescriptor));
            }

            if (!replaceAll)
            {
                var implementationType = serviceDescriptor.GetImplementationType();

                services.RemoveAll(s=>s.ServiceType==serviceDescriptor.ServiceType && s.GetImplementationType()==implementationType);

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
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceDescriptor"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceEnumerable<TService, TImplementation>(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
           where TService : class where TImplementation : class, TService
        {
            return services.ReplaceEnumerable(ServiceDescriptor.Transient<TService, TImplementation>(), serviceDescriptor);
        }

        /// <summary>
        /// Removes the first service in <see cref="IServiceCollection"/> with the same service type
        /// as <paramref name="sourcedescriptor"/> and adds <paramef name="descriptor"/> to the collection.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="sourcedescriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <param name="distdescriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <returns></returns>
        public static IServiceCollection ReplaceEnumerable(
            this IServiceCollection collection,
            ServiceDescriptor sourcedescriptor, ServiceDescriptor distdescriptor)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (sourcedescriptor == null)
            {
                throw new ArgumentNullException(nameof(sourcedescriptor));
            }

            var implementationType = sourcedescriptor.GetImplementationType();

            if (implementationType == typeof(object) ||
                implementationType == sourcedescriptor.ServiceType)
            {
                throw new ArgumentException($"Implementation type cannot be '{implementationType}' because it is indistinguishable from other services registered for '{sourcedescriptor.ServiceType}'.", nameof(sourcedescriptor));
            }


            var registeredServiceDescriptor = collection.FirstOrDefault(s => s.ServiceType == sourcedescriptor.ServiceType &&
                              s.GetImplementationType() == implementationType);
            if (registeredServiceDescriptor != null)
            {
                collection.Remove(registeredServiceDescriptor);
            }

            collection.Add(distdescriptor);
            return collection;
        }

        /// <summary>
        /// Removes the first service in <see cref="IServiceCollection"/> with the same service type
        /// as <paramref name="descriptor"/> and adds <paramef name="descriptor"/> to the collection.
        /// </summary>
        /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to replace with.</param>
        /// <returns></returns>
        public static IServiceCollection RemoveEnumerable(
            this IServiceCollection collection,
            ServiceDescriptor descriptor)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var implementationType = descriptor.GetImplementationType();

            if (implementationType == typeof(object) ||
                implementationType == descriptor.ServiceType)
            {
                throw new ArgumentException($"Implementation type cannot be '{implementationType}' because it is indistinguishable from other services registered for '{descriptor.ServiceType}'.", nameof(descriptor));
            }


            var registeredServiceDescriptor = collection.FirstOrDefault(s => s.ServiceType == descriptor.ServiceType &&
                              s.GetImplementationType() == implementationType);
            if (registeredServiceDescriptor != null)
            {
                collection.Remove(registeredServiceDescriptor);
            }
            return collection;
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
