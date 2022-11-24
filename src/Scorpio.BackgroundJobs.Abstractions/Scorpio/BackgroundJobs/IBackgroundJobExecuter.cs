using System.Threading.Tasks;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBackgroundJobExecuter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ExecuteAsync(JobExecutionContext context);
    }
}