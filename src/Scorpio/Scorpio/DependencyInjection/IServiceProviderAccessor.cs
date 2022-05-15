using System;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceProviderAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
