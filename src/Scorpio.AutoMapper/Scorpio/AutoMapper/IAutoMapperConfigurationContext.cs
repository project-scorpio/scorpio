using System;

using AutoMapper;

namespace Scorpio.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAutoMapperConfigurationContext
    {
        /// <summary>
        /// 
        /// </summary>
        IMapperConfigurationExpression MapperConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}