using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

using AspectCore.Extensions.Reflection;

namespace Scorpio.DependencyInjection
{
    internal static class PropertyInjectionUtils
    {
        private static readonly ConcurrentDictionary<Type, bool> _dictionary = new ConcurrentDictionary<Type, bool>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = " <挂起>")]
        public static bool TypeRequired(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return _dictionary.GetOrAdd(type, t => t.GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.CanWrite).Any(x => !x.GetReflector().IsDefined<NotAutowiredAttribute>()));
        }


    }
}
