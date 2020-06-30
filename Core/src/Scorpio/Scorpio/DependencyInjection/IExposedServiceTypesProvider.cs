using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExposedServiceTypesProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Type[] GetExposedServiceTypes(Type targetType);

        /// <summary>
        /// 
        /// </summary>
        ServiceLifetime ServiceLifetime { get; }
    }
}
