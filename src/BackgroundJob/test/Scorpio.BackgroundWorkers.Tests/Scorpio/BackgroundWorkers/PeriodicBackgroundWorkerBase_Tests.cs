using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundWorkers
{
    public class PeriodicBackgroundWorkerBase_Tests : BackgroundWorkersTestBase
    {
        [Fact]
        public async System.Threading.Tasks.Task StartAsync()
        {
            var worker = GetService<FakePeriodicBackgroundWorker>();
            await worker.StartAsync();
            await Task.Delay(1000);
            worker.InvokeCount.ShouldBe(1);
            await worker.StopAsync();
        }
    }
}
