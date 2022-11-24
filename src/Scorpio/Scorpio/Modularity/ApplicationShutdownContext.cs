using System;

using Scorpio.DependencyInjection;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationShutdownContext:IServiceProviderAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        internal ApplicationShutdownContext(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
    }
}