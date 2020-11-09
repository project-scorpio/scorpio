using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// Provides the abstract base class for a background worker.
    /// </summary>
    public abstract class BackgroundWorkerBase : IBackgroundWorker
    {

        /// <summary>
        /// Gets or sets the <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.
        /// </summary>
        /// <value>The <see cref="ILogger{TCategoryName}"/> used to log messages from the background job.</value>
        public ILogger<BackgroundWorkerBase> Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerBase"/> class.
        /// </summary>
        protected BackgroundWorkerBase()
        {
            Logger = NullLogger<BackgroundWorkerBase>.Instance;
        }

        /// <summary>
        /// Start the current background worker asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Indicates if the worker startup should be aborted.</param>
        /// <returns>That represents the asynchronous execution.</returns>
        public virtual Task StartAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogDebug("Started background worker: " + ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop processing work and shut down the background worker, gracefully if possible.
        /// </summary>
        /// <param name="cancellationToken">Indicates if the graceful shutdown should be aborted.</param>
        /// <returns>That represents the asynchronous execution.</returns>
        public virtual Task StopAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogDebug("Stopped background worker: " + ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns> A string that represents the current object.</returns>
        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}
