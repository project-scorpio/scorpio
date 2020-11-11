using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using AspectCore.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    internal static class ServiceDefinitionExtensions
    {
        internal static bool RequiredPropertyInjection(this ServiceDefinition serviceDefinition)
        {
            if (serviceDefinition.GetType().Name=="ProxyServiceDefinition" && serviceDefinition.ServiceType.GetTypeInfo().IsInterface)
            {
                return false;
            }
            var implType = serviceDefinition.GetImplementationType();
            if (implType == null)
            {
                return false;
            }
            if (implType == typeof(object))
            {
                return true;
            }
            return PropertyInjectionUtils.TypeRequired(implType);
        }

        internal static Type GetImplementationType(this ServiceDefinition serviceDefinition)
        {
            if (serviceDefinition is TypeServiceDefinition typeServiceDefinition)
            {
                return typeServiceDefinition.ImplementationType;
            }
            else if (serviceDefinition is InstanceServiceDefinition instanceServiceDefinition)
            {
                return instanceServiceDefinition.ImplementationInstance.GetType();
            }
            else if (serviceDefinition is DelegateServiceDefinition delegaetServiceDefinition)
            {
                var typeArguments = delegaetServiceDefinition.ImplementationDelegate.GetType().GenericTypeArguments;

                return typeArguments[1];
            }
            else if (serviceDefinition.GetType().Name=="ProxyServiceDefinition")
            {
                return ((dynamic)serviceDefinition).ProxyType;
            }
            return null;
        }
    }
}
