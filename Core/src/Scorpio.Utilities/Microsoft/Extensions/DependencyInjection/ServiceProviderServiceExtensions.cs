using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceProviderServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceProvider serviceProvider, Func<T> defaultValue)
        {
            var result = serviceProvider.GetService<T>() ?? defaultValue();
            return result;
        }
    }
}
