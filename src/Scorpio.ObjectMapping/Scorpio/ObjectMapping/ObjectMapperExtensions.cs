using System;
using System.Reflection;

namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectMapperExtensions
    {
        private static readonly MethodInfo _mapToNewObjectMethod;
        private static readonly MethodInfo _mapToExistingObjectMethod;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "<Pending>")]
        static ObjectMapperExtensions()
        {
            var methods = typeof(IObjectMapper).GetMethods();
            foreach (var method in methods)
            {
                if (method.Name == nameof(IObjectMapper.Map) && method.IsGenericMethodDefinition)
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length == 1)
                    {
                        _mapToNewObjectMethod = method;
                    }
                    else if (parameters.Length == 2)
                    {
                        _mapToExistingObjectMethod = method;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectMapper"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object Map(this IObjectMapper objectMapper, Type sourceType, Type destinationType, object source)
        {
            return _mapToNewObjectMethod
                .MakeGenericMethod(sourceType, destinationType)
                .Invoke(objectMapper, new[] { source });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectMapper"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static object Map(this IObjectMapper objectMapper, Type sourceType, Type destinationType, object source, object destination)
        {
            return _mapToExistingObjectMethod
                .MakeGenericMethod(sourceType, destinationType)
                .Invoke(objectMapper, new[] { source, destination });
        }
    }
}
