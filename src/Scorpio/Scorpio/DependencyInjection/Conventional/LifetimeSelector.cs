using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class LifetimeSelector(ServiceLifetime lifetime) : IRegisterAssemblyLifetimeSelector
    {
        public static LifetimeSelector Transient { get; } = new LifetimeSelector(ServiceLifetime.Transient);
        public static LifetimeSelector Scoped { get; } = new LifetimeSelector(ServiceLifetime.Scoped);
        public static LifetimeSelector Singleton { get; } = new LifetimeSelector(ServiceLifetime.Singleton);

        public static LifetimeSelector GetSelector(ServiceLifetime serviceLifetime) => serviceLifetime switch
        {
            ServiceLifetime.Singleton => Singleton,
            ServiceLifetime.Scoped => Scoped,
            ServiceLifetime.Transient => Transient,
            _ => throw new NotImplementedException(),
        };

        private readonly ServiceLifetime _lifetime = lifetime;

        public ServiceLifetime Select(Type componentType) => _lifetime;


    }

    internal class ExposeLifetimeSelector : IRegisterAssemblyLifetimeSelector
    {
        public static ExposeLifetimeSelector Instance { get; } = new ExposeLifetimeSelector();

        public ServiceLifetime Select(Type componentType)
        {
            var attr = componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr?.ServiceLifetime ?? ServiceLifetime.Transient;
        }
    }
}
