using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz
{
    /// <summary>
    /// 
    /// </summary>
    public static class SchedulerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="scheduler"></param>
        /// <param name="configureJob"></param>
        /// <param name="configureTrigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ScheduleJob<TJob>(this IScheduler scheduler, Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger, CancellationToken cancellationToken = default) where TJob : IJob
        {

            var detail = JobBuilder.Create<TJob>().Action(configureJob).Build();
            var trigger = TriggerBuilder.Create().Action(configureTrigger).Build();
            return scheduler.ScheduleJob(detail, trigger, cancellationToken);

        }
    }
}
