using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Scorpio.BackgroundWorkers;
using Scorpio.Modularity;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(BackgroundJobsAbstractionsModule),
        typeof(BackgroundWorkersModule)
        )]
    public class BackgroundJobsModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Initialize(ApplicationInitializationContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<BackgroundJobOptions>>().Value;
            if (options.IsJobExecutionEnabled)
            {
                context.ServiceProvider
                    .GetRequiredService<IBackgroundWorkerManager>()
                    .Add(
                        context.ServiceProvider
                            .GetRequiredService<IBackgroundJobWorker>()
                    );
            }
        }

    }
}