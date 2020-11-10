using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Scorpio.BackgroundWorkers;
using Scorpio.Threading;
using Scorpio.Timing;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobWorker : AsyncPeriodicBackgroundWorkerBase, IBackgroundJobWorker
    {
        /// <summary>
        /// 
        /// </summary>
        protected BackgroundJobOptions JobOptions { get; }

        /// <summary>
        /// 
        /// </summary>
        protected BackgroundJobWorkerOptions WorkerOptions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="jobOptions"></param>
        /// <param name="workerOptions"></param>
        /// <param name="serviceScopeFactory"></param>
        public BackgroundJobWorker(
            IScorpioTimer timer,
            IOptions<BackgroundJobOptions> jobOptions,
            IOptions<BackgroundJobWorkerOptions> workerOptions,
            IServiceScopeFactory serviceScopeFactory)
            : base(
                timer,
                serviceScopeFactory)
        {
            WorkerOptions = workerOptions.Value;
            JobOptions = jobOptions.Value;
            Timer.Period = WorkerOptions.JobPollPeriod;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerContext"></param>
        /// <returns></returns>
        protected override async Task DoWorkAsync(BackgroundWorkerContext workerContext)
        {
            var store = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobStore>();

            var waitingJobs = await store.GetWaitingJobsAsync(WorkerOptions.MaxJobFetchCount);

            if (!waitingJobs.Any())
            {
                return;
            }

            var jobExecuter = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobExecuter>();
            var clock = workerContext.ServiceProvider.GetRequiredService<IClock>();
            var serializer = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobSerializer>();

            foreach (var jobInfo in waitingJobs)
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = clock.Now;

                try
                {
                    var jobConfiguration = JobOptions.GetJob(jobInfo.JobName);
                    var jobArgs = serializer.Deserialize(jobInfo.JobArgs, jobConfiguration.ArgsType);
                    var context = new JobExecutionContext(workerContext.ServiceProvider, jobConfiguration.JobType, jobArgs);

                    try
                    {
                        await jobExecuter.ExecuteAsync(context);

                        await store.DeleteAsync(jobInfo.Id);
                    }
                    catch (BackgroundJobExecutionException)
                    {
                        var nextTryTime = CalculateNextTryTime(jobInfo, clock);

                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        await TryUpdateAsync(store, jobInfo);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                    jobInfo.IsAbandoned = true;
                    await TryUpdateAsync(store, jobInfo);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        protected virtual async Task TryUpdateAsync(IBackgroundJobStore store, BackgroundJobInfo jobInfo)
        {
            try
            {
                await store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.LogException(updateEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <param name="clock"></param>
        /// <returns></returns>
        protected virtual DateTime? CalculateNextTryTime(BackgroundJobInfo jobInfo, IClock clock)
        {
            var nextWaitDuration = WorkerOptions.DefaultFirstWaitDuration * Math.Pow(WorkerOptions.DefaultWaitFactor, jobInfo.TryCount - 1);
            var nextTryDate = jobInfo.LastTryTime?.AddSeconds(nextWaitDuration) ??
                              clock.Now.AddSeconds(nextWaitDuration);

            if (nextTryDate.Subtract(jobInfo.CreationTime).TotalSeconds > WorkerOptions.DefaultTimeout)
            {
                return null;
            }

            return nextTryDate;
        }
    }
}