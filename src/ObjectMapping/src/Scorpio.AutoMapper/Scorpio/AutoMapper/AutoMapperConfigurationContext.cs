using System;

using AutoMapper;

namespace Scorpio.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfigurationContext : IAutoMapperConfigurationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IMapperConfigurationExpression MapperConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapperConfigurationExpression"></param>
        /// <param name="serviceProvider"></param>
        public AutoMapperConfigurationContext(
            IMapperConfigurationExpression mapperConfigurationExpression,
            IServiceProvider serviceProvider)
        {
            MapperConfiguration = Check.NotNull(mapperConfigurationExpression, nameof(mapperConfigurationExpression));
            ServiceProvider = Check.NotNull(serviceProvider, nameof(serviceProvider));
        }
    }
}