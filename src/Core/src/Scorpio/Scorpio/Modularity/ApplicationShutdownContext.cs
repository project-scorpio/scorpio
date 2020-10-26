using System;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationShutdownContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        internal ApplicationShutdownContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}