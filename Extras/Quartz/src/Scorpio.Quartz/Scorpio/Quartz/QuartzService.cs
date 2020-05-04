using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Quartz
{
    class QuartzService : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly IScheduler scheduler;

        public QuartzService(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await scheduler.Shutdown(cancellationToken);
        }
    }
}
