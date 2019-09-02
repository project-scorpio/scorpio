using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConnectionStringResolverExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public static string Resolve<T>(this IConnectionStringResolver resolver)
        {
            return resolver.Resolve(ConnectionStringNameAttribute.GetConnStringName<T>());
        }
    }
}
