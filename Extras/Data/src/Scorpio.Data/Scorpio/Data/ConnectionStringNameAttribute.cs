
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStringNameAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionStringNameAttribute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ConnectionStringNameAttribute( string name)
        {
            Check.NotNull(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetConnStringName<T>()
        {
            return GetConnStringName(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConnStringName(Type type)
        {
            var nameAttribute = type.GetTypeInfo().GetCustomAttribute<ConnectionStringNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.Name;
        }
    }
}
