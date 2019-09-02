using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExposeServicesAttribute : Attribute, IExposedServiceTypesProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public Type[] ExposedServiceTypes { get; }

        /// <summary>
        /// 
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exposedServiceTypes"></param>
        public ExposeServicesAttribute(params Type[] exposedServiceTypes)
        {
            ExposedServiceTypes = exposedServiceTypes ?? new Type[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public Type[] GetExposedServiceTypes(Type targetType)
        {
            return ExposedServiceTypes;
        }
    }
}
