using System;
using System.Threading.Tasks;

using Scorpio.DependencyInjection;
using Scorpio.Timing;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Default implementation of <see cref="IBackgroundJobManager"/>.
    /// </summary>
    public class DefaultBackgroundJobManager : IBackgroundJobManager, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected IClock Clock { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IBackgroundJobSerializer Serializer { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IBackgroundJobStore Store { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clock"></param>
        /// <param name="serializer"></param>
        /// <param name="store"></param>
        public DefaultBackgroundJobManager(
            IClock clock,
            IBackgroundJobSerializer serializer,
            IBackgroundJobStore store)
        {
            Clock = clock;
            Serializer = serializer;
            Store = store;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="args"></param>
        /// <param name="priority"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public virtual async Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            var jobName = BackgroundJobNameAttribute.GetName<TArgs>();
            var jobId = await EnqueueAsync(jobName, args, priority, delay);
            return jobId.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="args"></param>
        /// <param name="priority"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        protected virtual async Task<Guid> EnqueueAsync(string jobName, object args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            var jobInfo = new BackgroundJobInfo
            {
                Id = Guid.NewGuid(),
                JobName = jobName,
                JobArgs = Serializer.Serialize(args),
                Priority = priority,
                CreationTime = Clock.Now,
                NextTryTime = Clock.Now
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await Store.InsertAsync(jobInfo);

            return jobInfo.Id;
        }
    }
}