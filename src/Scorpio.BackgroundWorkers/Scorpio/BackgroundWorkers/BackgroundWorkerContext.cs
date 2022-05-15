using System;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundWorkerContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public BackgroundWorkerContext(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
    }
}
