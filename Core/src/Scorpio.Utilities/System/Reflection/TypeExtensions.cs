using System;
using System.Collections.Generic;
using System.Text;

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
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (@namespace == null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

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
        public static bool IsInNamespaceOf<T>(this Type @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return IsInNamespace(@this, typeof(T).Namespace);
        }



        /// <summary>
        /// Determines whether this type is assignable to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to test assignability to.</typeparam>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is assignable to references of type
        /// <typeparamref name="T"/>; otherwise, False.</returns>
        public static bool IsAssignableTo<T>(this Type @this)
        {
            return @this.IsAssignableTo(typeof(T));
        }

        /// <summary>
        /// Determines whether this type is assignable to <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to test assignability to.</param>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is assignable to references of type</returns>
        public static bool IsAssignableTo(this Type @this, Type type)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

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

    }
}

