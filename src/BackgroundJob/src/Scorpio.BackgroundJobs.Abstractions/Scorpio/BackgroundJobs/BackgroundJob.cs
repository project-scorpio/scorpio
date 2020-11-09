using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Provides the abstract base class for a synchronize background job.
    /// </summary>
    /// <typeparam name="TArgs">The args for the background job execution.</typeparam>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {

        /// <summary>
        /// Gets or sets the <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.
        /// </summary>
        /// <value>The <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.</value>
        public ILogger<BackgroundJob<TArgs>> Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJob{TArgs}"/> class.
        /// </summary>
        protected BackgroundJob()
        {
            Logger = NullLogger<BackgroundJob<TArgs>>.Instance;
        }

        /// <summary>
        /// Synchronize executes the background job using the specified args.
        /// </summary>
        /// <param name="args">The args for the current job execution.</param>
        public abstract void Execute(TArgs args);
    }
}