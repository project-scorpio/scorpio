using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class LifetimeSelector : IRegisterAssemblyLifetimeSelector
    {
        private readonly ServiceLifetime _lifetime;

        public LifetimeSelector(ServiceLifetime lifetime)
        {
            _lifetime = lifetime;
        }
        public ServiceLifetime Select(Type componentType)
        {
            return _lifetime;
        }
    }

    internal class ExposeLifetimeSelector : IRegisterAssemblyLifetimeSelector
    {
        public ServiceLifetime Select(Type componentType)
        {
            var attr = componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr?.ServiceLifetime ?? ServiceLifetime.Transient;
        }
    }
}
