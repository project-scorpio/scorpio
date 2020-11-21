using System.Linq;

using AspectCore.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.DependencyInjection
{
    internal class PropertyInjectorCallback : IServiceResolveCallback
    {

        public object Invoke(IServiceResolver resolver, object instance, ServiceDefinition service)
        {
            if (instance is IModuleContainer)
            {
                return instance;
            }
            var c = resolver.Resolve<IModuleContainer>();
            if (instance == null || !c.Modules.Any(m => m.Assembly == instance.GetType().Assembly) || !service.RequiredPropertyInjection())
            {
                return instance;
            }

            var injectorFactory = resolver.Resolve<IPropertyInjectorFactory>();
            var injector = injectorFactory.Create(instance.GetType());
            injector.Invoke(instance);
            return instance;
        }
    }
}
