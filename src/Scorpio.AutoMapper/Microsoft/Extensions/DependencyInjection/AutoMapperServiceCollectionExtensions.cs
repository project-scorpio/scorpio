using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.AutoMapper;
using Scorpio.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperObjectMapper(this IServiceCollection services)
        {
            return services.Replace(
                ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperObjectMapper<TContext>(this IServiceCollection services)
        {
            return services.Replace(
                ServiceDescriptor.Transient<IAutoObjectMappingProvider<TContext>, AutoMapperAutoObjectMappingProvider<TContext>>()
            );
        }
    }
}
