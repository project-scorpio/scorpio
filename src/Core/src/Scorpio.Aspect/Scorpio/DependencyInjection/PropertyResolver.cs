using System;

using AspectCore.Extensions.Reflection;

namespace Scorpio.DependencyInjection
{
    internal sealed class PropertyResolver
    {
        private readonly Func<IServiceProvider, object> _propertyFactory;
        private readonly PropertyReflector _reflector;

        internal PropertyResolver(Func<IServiceProvider, object> propertyFactory, PropertyReflector reflector)
        {
            _propertyFactory = propertyFactory;
            _reflector = reflector;
        }

        public void Resolve(IServiceProvider provider, object implementation)
        {
            var value = _propertyFactory(provider);
            if (value != null) { _reflector.SetValue(implementation, value); }
        }
    }
}
