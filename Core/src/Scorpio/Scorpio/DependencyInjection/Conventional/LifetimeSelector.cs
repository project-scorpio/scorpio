using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
