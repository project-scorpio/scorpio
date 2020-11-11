using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using AspectCore.Extensions.Reflection;

namespace Scorpio.DependencyInjection
{
    internal static class PropertyInjectionUtils
    {
        private readonly static ConcurrentDictionary<Type, bool> _dictionary = new ConcurrentDictionary<Type, bool>();

        public static bool TypeRequired(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return _dictionary.GetOrAdd(type, _ => type.GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.CanWrite).Any(x => !x.GetReflector().IsDefined<NotAutowiredAttribute>()));
        }

      
    }
}
