using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Scorpio.DependencyInjection;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobExecuter : IBackgroundJobExecuter, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger<BackgroundJobExecuter> Logger { protected get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public BackgroundJobExecuter()
        {

            Logger = NullLogger<BackgroundJobExecuter>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ExecuteAsync(JobExecutionContext context)
        {
            var job = context.ServiceProvider.GetService(context.JobType);
            if (job == null)
            {
                throw new ScorpioException("The job type is not registered to DI: " + context.JobType);
            }
            var jobExecuteMethod = context.JobType.GetMethod(nameof(IBackgroundJob<object>.Execute)) ?? 
                                   context.JobType.GetMethod(nameof(IAsyncBackgroundJob<object>.ExecuteAsync));
            if (jobExecuteMethod == null)
            {
                throw new ScorpioException($"Given job type does not implement {typeof(IBackgroundJob<>).Name} or {typeof(IAsyncBackgroundJob<>).Name}. " +
                                       "The job type was: " + context.JobType);
            }

            try
            {
                if (jobExecuteMethod.Name == nameof(IAsyncBackgroundJob<object>.ExecuteAsync))
                {
                    await ((Task) jobExecuteMethod.Invoke(job, new[] {context.JobArgs}));
                }
                else
                {
                    jobExecuteMethod.Invoke(job, new[] { context.JobArgs });
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);

                throw new BackgroundJobExecutionException("A background job execution is failed. See inner exception for details.", ex)
                {
                    JobType = context.JobType.AssemblyQualifiedName,
                    JobArgs = context.JobArgs
                };
            }
        }
    }
}