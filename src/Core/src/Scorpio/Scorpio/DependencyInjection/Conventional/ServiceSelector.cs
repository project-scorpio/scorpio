using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class DefaultInterfaceSelector : IRegisterAssemblyServiceSelector
    {
        public static DefaultInterfaceSelector Instance { get; } = new DefaultInterfaceSelector();
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
        public static AllInterfaceSelector Instance { get; } = new AllInterfaceSelector();
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
        public static SelfSelector Instance { get; } = new SelfSelector();
        public IEnumerable<Type> Select(Type componentType) => new Type[] { componentType };
    }

    internal class TypeSelector<T> : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType) => new Type[] { typeof(T) };
    }

    internal class ExposeServicesSelector : IRegisterAssemblyServiceSelector
    {
        public static ExposeServicesSelector Instance { get; } = new ExposeServicesSelector();
        public IEnumerable<Type> Select(Type componentType)
        {
            var attr = componentType.GetAttribute<ExposeServicesAttribute>(inherit:true);
            return attr.GetExposedServiceTypes(componentType);
        }
    }
}
