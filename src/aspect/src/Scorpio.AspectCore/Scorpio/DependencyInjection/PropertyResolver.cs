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
            _propertyFactory =Check.NotNull( propertyFactory,nameof(propertyFactory));
            _reflector =Check.NotNull( reflector,nameof(reflector));
        }

        public void Resolve(IServiceProvider provider, object implementation)
        {
            Check.NotNull(provider,nameof(provider));
            Check.NotNull(implementation,nameof(implementation));
            var value = _propertyFactory(provider);
            if (value != null) { _reflector.SetValue(implementation, value); }
        }
    }
}
