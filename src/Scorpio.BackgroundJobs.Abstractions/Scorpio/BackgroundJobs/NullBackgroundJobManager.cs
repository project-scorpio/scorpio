using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Scorpio.DependencyInjection;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class NullBackgroundJobManager : IBackgroundJobManager, ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger<NullBackgroundJobManager> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NullBackgroundJobManager() => Logger = NullLogger<NullBackgroundJobManager>.Instance;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="priority"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) => throw new ScorpioException("Background job system has not a real implementation. If it's mandatory, use an implementation (either the default provider or a 3rd party implementation). If it's optional, check IBackgroundJobManager.IsAvailable() extension method and act based on it.");
    }
}