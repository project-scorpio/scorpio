using System;

using AspectCore.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    internal class PropertyInjectorFactory : IPropertyInjectorFactory
    {
        private readonly IServiceProvider _servicePorvider;
        private readonly PropertyResolverSelector _propertyResolverSelector;


        public PropertyInjectorFactory(IServiceProvider servicePorvider)
        {
            _servicePorvider = servicePorvider;
            _propertyResolverSelector = PropertyResolverSelector.Default;

        }

        public IPropertyInjector Create(Type implementationType) => new PropertyInjector(_servicePorvider, _propertyResolverSelector.SelectPropertyResolver(implementationType));
    }
}
