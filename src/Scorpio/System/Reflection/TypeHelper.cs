using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Scorpio;
using Scorpio.Localization;

namespace System.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeHelper
    {

        private static readonly HashSet<Type> _floatingTypes = new HashSet<Type>
        {
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        private static readonly HashSet<Type> _nonNullablePrimitiveTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(bool),
            typeof(float),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNonNullablePrimitiveType(Type type)
        {

            return _nonNullablePrimitiveTypes.Contains(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsFunc(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsFunc<TReturn>(object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="includeNullables"></param>
        /// <param name="includeEnums"></param>
        /// <returns></returns>
        public static bool IsPrimitiveExtended(Type type, bool includeNullables = true, bool includeEnums = false)
        {
            if (IsPrimitiveExtendedInternal(type, includeEnums))
            {
                return true;
            }

            if (includeNullables && IsNullable(type) && type.GenericTypeArguments.Any())
            {
                return IsPrimitiveExtendedInternal(type.GenericTypeArguments[0], includeEnums);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type UnWrapNullable(this Type t)
        {
            if (t.GetGenericArguments().Length > 0 && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return t.GetGenericArguments().FirstOrDefault();
            }

            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="itemType"></param>
        /// <param name="includePrimitives"></param>
        /// <returns></returns>
        public static bool IsEnumerable(Type type, out Type itemType, bool includePrimitives = true)
        {
            if (!includePrimitives && IsPrimitiveExtended(type))
            {
                itemType = null;
                return false;
            }

            var enumerableTypes = type.GetAssignableToGenericTypes(typeof(IEnumerable<>));
            if (enumerableTypes.Count == 1)
            {
                itemType = enumerableTypes[0].GenericTypeArguments[0];
                return true;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                itemType = typeof(object);
                return true;
            }

            itemType = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static bool IsDictionary(Type type, out Type keyType, out Type valueType)
        {
            var dictionaryTypes = type.GetAssignableToGenericTypes(typeof(IDictionary<,>));

            if (dictionaryTypes.Count == 1)
            {
                keyType = dictionaryTypes[0].GenericTypeArguments[0];
                valueType = dictionaryTypes[0].GenericTypeArguments[1];
                return true;
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                keyType = typeof(object);
                valueType = typeof(object);
                return true;
            }

            keyType = null;
            valueType = null;

            return false;
        }

        private static bool IsPrimitiveExtendedInternal(Type type, bool includeEnums)
        {
            if (type.IsPrimitive)
            {
                return true;
            }

            if (includeEnums && type.IsEnum)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDefaultValue<T>()
        {
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

    }
}
