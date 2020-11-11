using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class DefaultInterfaceSelector : IRegisterAssemblyServiceSelector
    {
        public DefaultInterfaceSelector()
        {
        }

        public IEnumerable<Type> Select(Type componentType)
        {
            var services = componentType.GetInterfaces().Where(s => componentType.Name.EndsWith(s.Name.RemovePreFix("I"))).ToList();
            services.Add(componentType);
            return services;
        }
    }

    internal class AllInterfaceSelector : IRegisterAssemblyServiceSelector
    {
        public AllInterfaceSelector()
        {
        }

        public IEnumerable<Type> Select(Type componentType)
        {
            var services = componentType.GetInterfaces().Where(s => s.IsPublic).ToList();
            services.Add(componentType);
            return services;
        }
    }

    internal class SelfSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType) => new Type[] { componentType };
    }

    internal class TypeSelector<T> : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType) => new Type[] { typeof(T) };
    }

    internal class ExposeServicesSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            var attr = componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr.GetExposedServiceTypes(componentType);
        }
    }
}
