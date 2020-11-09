using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.Threading;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundWorkers
{
    public class PeriodicBackgroundWorkerBase_Tests : BackgroundWorkersTestBase
    {
        protected override Bootstrapper CreateBootstrapper(IServiceCollection services)
        {
            services.Configure<BackgroundWorkerOptions>(o => o.IsEnabled = false);

            return base.CreateBootstrapper(services);
        }

        [Fact]
        public async System.Threading.Tasks.Task StartAsync()
        {
            var invokeCount = 0;
            var executor = GetService<WorkerExecutor>();
            executor.Action = c => invokeCount++;
            var worker = GetService<FakePeriodicBackgroundWorker>();
            await worker.StartAsync();
            await Task.Delay(1000);
            invokeCount.ShouldBe(1);
            await worker.StopAsync();
            executor.Action = c => throw new NotImplementedException();
            Should.NotThrow(() =>worker.StartAsync());
        }
    }
}
