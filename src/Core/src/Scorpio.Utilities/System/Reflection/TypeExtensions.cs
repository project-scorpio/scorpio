﻿using System.Collections.Generic;

using Scorpio;

namespace System.Reflection
{
    /// <summary>
    /// Extends <see cref="Type"/> with methods .
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns true if this type is in the <paramref name="namespace"/> namespace
        /// or one of its sub-namespaces.
        /// </summary>
        /// <param name="this">The type to test.</param>
        /// <param name="namespace">The namespace to test.</param>
        /// <returns>True if this type is in the <paramref name="namespace"/> namespace
        /// or one of its sub-namespaces; otherwise, false.</returns>
        public static bool IsInNamespace(this Type @this, string @namespace)
        {
            Check.NotNull(@this, nameof(@this));
            Check.NotNull(@namespace, nameof(@namespace));

            return @this.Namespace != null &&
                (@this.Namespace == @namespace || @this.Namespace.StartsWith(@namespace + ".", StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns true if this type is in the same namespace as <typeparamref name="T"/>
        /// or one of its sub-namespaces.
        /// </summary>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is in the same namespace as <typeparamref name="T"/>
        /// or one of its sub-namespaces; otherwise, false.</returns>
        public static bool IsInNamespaceOf<T>(this Type @this) => IsInNamespace(@this, typeof(T).Namespace);



        /// <summary>
        /// Determines whether this type is assignable to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to test assignability to.</typeparam>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is assignable to references of type
        /// <typeparamref name="T"/>; otherwise, False.</returns>
        public static bool IsAssignableTo<T>(this Type @this) => @this.IsAssignableTo(typeof(T));

        /// <summary>
        /// Determines whether this type is assignable to <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to test assignability to.</param>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is assignable to references of type</returns>
        public static bool IsAssignableTo(this Type @this, Type type)
        {
            Check.NotNull(@this, nameof(@this));
            Check.NotNull(type, nameof(type));
            return type.GetTypeInfo().IsAssignableFrom(@this.GetTypeInfo());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(this Type @this, Type genericType)
        {
            Check.NotNull(@this, nameof(@this));
            Check.NotNull(genericType, nameof(genericType));
            var givenTypeInfo = @this.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && @this.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenTypeInfo.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAssignableToGenericTypes(this Type @this, Type genericType)
        {
            Check.NotNull(@this, nameof(@this));
            Check.NotNull(genericType, nameof(genericType));
            var givenTypeInfo = @this.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && @this.GetGenericTypeDefinition() == genericType)
            {
                yield return @this;
            }

            foreach (var interfaceType in givenTypeInfo.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    yield return interfaceType;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                yield break;
            }
            foreach (var item in GetAssignableToGenericTypes(givenTypeInfo.BaseType, genericType))
            {
                yield return item;
            }
        }
        /// <summary>
        /// Determines whether this type is a standard type.
        /// </summary>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is is a standard type.</returns>
        public static bool IsStandardType(this Type @this)
        {
            Check.NotNull(@this, nameof(@this));
            return @this.IsClass && !@this.IsAbstract && !@this.IsGenericTypeDefinition;
        }

    }
}

