using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
