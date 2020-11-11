using System;

using AspectCore.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    internal class PropertyInjector : IPropertyInjector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PropertyResolver[] _propertyResolvers;

        public PropertyInjector(IServiceProvider serviceProvider, PropertyResolver[] propertyResolvers)
        {
            _serviceProvider = serviceProvider;
            _propertyResolvers = propertyResolvers;
        }

        public void Invoke(object implementation)
        {
            if (implementation == null || _propertyResolvers.Length == 0)
            {
                return;
            }

            for (var i = 0; i < _propertyResolvers.Length; i++)
            {
                _propertyResolvers[i].Resolve(_serviceProvider, implementation);
            }
        }
    }
}
