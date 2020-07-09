using System.Threading;
using System.Threading.Tasks;

using Quartz;

namespace Scorpio.Quartz
{
    class QuartzService : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly IScheduler _scheduler;

        public QuartzService(IScheduler scheduler)
        {
            this._scheduler = scheduler;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}
