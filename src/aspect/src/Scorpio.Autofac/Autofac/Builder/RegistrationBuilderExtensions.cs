using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autofac.Core;
using Autofac.Extras.DynamicProxy;

using Scorpio;
using Scorpio.Castle.DynamicProxy;
using Scorpio.DependencyInjection;
using Scorpio.DynamicProxy;
using Scorpio.Modularity;
namespace Autofac.Builder
{
    internal static class RegistrationBuilderExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> ConfigureConventions<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
                IModuleContainer moduleContainer,
                ServiceInterceptorList serviceInterceptorList)
            where TActivatorData : ReflectionActivatorData
        {
            Check.NotNull(moduleContainer,nameof(moduleContainer));
            Check.NotNull(registrationBuilder,nameof(registrationBuilder));
            Check.NotNull(serviceInterceptorList,nameof(serviceInterceptorList));
            var serviceType = registrationBuilder.RegistrationData.Services.OfType<IServiceWithType>().FirstOrDefault()?.ServiceType;
            if (serviceType != null)
            {
                var implementationType = registrationBuilder.ActivatorData.ImplementationType;
                if (implementationType != null)
                {
                    registrationBuilder = registrationBuilder.EnablePropertyInjection(moduleContainer, implementationType);
                    registrationBuilder = registrationBuilder.InvokeRegistrationActions(serviceInterceptorList, serviceType, implementationType);
                }
            }
            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InvokeRegistrationActions<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, ServiceInterceptorList serviceInterceptorList, Type serviceType, Type implementationType)
            where TActivatorData : ReflectionActivatorData
        {
            var interceptors = serviceInterceptorList.GetInterceptors(serviceType).Concat(serviceInterceptorList.GetInterceptors(implementationType));
            if (interceptors.Any())
            {
                registrationBuilder = registrationBuilder.AddInterceptors(
                    serviceType,
                    interceptors
                );
            }

            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnablePropertyInjection<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
                IModuleContainer moduleContainer,
                Type implementationType)
            where TActivatorData : ReflectionActivatorData
        {
            //Enable Property Injection only for types in an assembly containing an AbpModule
            if (moduleContainer.Modules.Any(m => m.Assembly == implementationType.Assembly))
            {
                registrationBuilder = registrationBuilder.PropertiesAutowired((p,o)=>!p.AttributeExists<NotAutowiredAttribute>());
            }

            return registrationBuilder;
        }

        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AddInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
            Type serviceType,
            IEnumerable<Type> interceptors)
            where TActivatorData : ReflectionActivatorData
        {
            if (serviceType.IsInterface)
            {
                registrationBuilder = registrationBuilder.EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions());
            }
            else
            {
                (registrationBuilder as IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, TRegistrationStyle>)?.EnableClassInterceptors();
            }

            foreach (var interceptor in interceptors)
            {
                registrationBuilder.InterceptedBy(
                    typeof(AsyncDeterminationInterceptor<>).MakeGenericType(interceptor)
                );
            }

            return registrationBuilder;
        }
    }
}
