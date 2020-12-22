using System;
using System.Reflection;

using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.ObjectMapping
{
    internal class ObjectMappingConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context) => context.RegisterConventionalDependencyInject(config => config.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableToGenericType(typeof(IObjectMapper<,>))).As(ObjectMappingServiceSelector.Instance));
    }
}
