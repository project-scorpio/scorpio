using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Provides the abstract base class for a asynchronous background job.
    /// </summary>
    /// <typeparam name="TArgs">The args for the background job execution.</typeparam>
    public abstract class AsyncBackgroundJob<TArgs> : IAsyncBackgroundJob<TArgs>
    {

        /// <summary>
        /// Gets or sets the <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.
        /// </summary>
        /// <value>The <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.</value>
        public ILogger<AsyncBackgroundJob<TArgs>> Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncBackgroundJob{TArgs}"/> class.
        /// </summary>
        protected AsyncBackgroundJob()
        {
            Logger = NullLogger<AsyncBackgroundJob<TArgs>>.Instance;
        }

        /// <summary>
        /// Asynchronously executes the background job using the specified args.
        /// </summary>
        /// <param name="args">The args for the current job execution.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous execution.</returns>
        public abstract Task ExecuteAsync(TArgs args);
    }
}