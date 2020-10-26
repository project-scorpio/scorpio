using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scorpio.DependencyInjection.Conventional
{
    class DefaultInterfaceSelector : IRegisterAssemblyServiceSelector
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

    class AllInterfaceSelector : IRegisterAssemblyServiceSelector
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


    class SelfSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            return new Type[] { componentType };
        }
    }

    class TypeSelector<T> : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            return new Type[] { typeof(T) };
        }
    }
    class ExposeServicesSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            var attr = componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr.GetExposedServiceTypes(componentType);
        }
    }
}
