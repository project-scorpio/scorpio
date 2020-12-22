using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.ObjectMapping
{
    internal class ObjectMappingConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.RegisterConventionalDependencyInject(config =>
            {
                config.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableToGenericType(typeof(IObjectMapper<,>))).As(ObjectMappingServiceSelector.Instance);
            });
        }
    }
}
