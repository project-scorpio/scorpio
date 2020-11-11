using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Scorpio.DependencyInjection;
using Scorpio.Timing;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore, ISingletonDependency
    {
        private readonly ConcurrentDictionary<Guid, BackgroundJobInfo> _jobs;

        /// <summary>
        /// 
        /// </summary>
        protected IClock Clock { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryBackgroundJobStore"/> class.
        /// </summary>
        public InMemoryBackgroundJobStore(IClock clock)
        {
            Clock = clock;
            _jobs = new ConcurrentDictionary<Guid, BackgroundJobInfo>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public virtual Task<BackgroundJobInfo> FindAsync(Guid jobId) => Task.FromResult(_jobs.GetOrDefault(jobId));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public virtual Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            _jobs[jobInfo.Id] = jobInfo;

            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public virtual Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var waitingJobs = _jobs.Values
                .Where(t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            return Task.FromResult(waitingJobs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(Guid jobId)
        {
            _jobs.TryRemove(jobId, out _);

            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo.IsAbandoned)
            {
                return DeleteAsync(jobInfo.Id);
            }

            return Task.FromResult(0);
        }
    }
}