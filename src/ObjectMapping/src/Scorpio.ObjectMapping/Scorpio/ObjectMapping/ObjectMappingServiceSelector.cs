using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.ObjectMapping
{
    internal class ObjectMappingServiceSelector : IRegisterAssemblyServiceSelector
    {
        public static ObjectMappingServiceSelector Instance { get; } = new ObjectMappingServiceSelector();
        public IEnumerable<Type> Select(Type componentType) => componentType.GetAssignableToGenericTypes(typeof(IObjectMapper<,>));
    }
}
