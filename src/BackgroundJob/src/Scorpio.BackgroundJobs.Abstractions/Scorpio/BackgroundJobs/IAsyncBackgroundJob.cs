using System.Threading.Tasks;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Defines interface of a background job.
    /// </summary>
    public interface IAsyncBackgroundJob<in TArgs>
    {
        /// <summary>
        /// Executes the job with the <typeparamref name="TArgs"/>.
        /// </summary>
        /// <param name="args">Job arguments.</param>
        Task ExecuteAsync(TArgs args);
    }
}