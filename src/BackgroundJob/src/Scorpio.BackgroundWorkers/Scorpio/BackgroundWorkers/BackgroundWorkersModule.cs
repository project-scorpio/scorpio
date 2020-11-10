
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Scorpio.Modularity;
using Scorpio.Threading;

namespace Scorpio.BackgroundWorkers
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundWorkersModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Initialize(ApplicationInitializationContext context)
        {
            var opt = context.ServiceProvider.GetRequiredService<IOptions<BackgroundWorkerOptions>>().Value;
            if (opt.IsEnabled)
            {
                AsyncHelper.RunSync(() =>
                    context.ServiceProvider.GetRequiredService<IBackgroundWorkerManager>().StartAsync()
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Shutdown(ApplicationShutdownContext context)
        {
            var opt = context.ServiceProvider.GetRequiredService<IOptions<BackgroundWorkerOptions>>().Value;
            if (opt.IsEnabled)
            {
                AsyncHelper.RunSync(() =>
                    context.ServiceProvider.GetRequiredService<IBackgroundWorkerManager>().StopAsync()
                );
            }
        }
    }
}
