using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundWorkers
{
    public class AsyncPeriodicBackgroundWorkerBase_Tests : BackgroundWorkersTestBase
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
            var executor = GetService<AsyncWorkerExecutor>();
            executor.ActionAsync = c => { invokeCount++; return Task.CompletedTask; };
            var worker = GetService<FakeAsyncPeriodicBackgroundWorkerBase>();
            await worker.StartAsync();
            await Task.Delay(1000);
            invokeCount.ShouldBe(1);
            await worker.StopAsync();
#pragma warning disable S3626 // Jump statements should not be redundant
            executor.ActionAsync = c => throw new NotImplementedException();
#pragma warning restore S3626 // Jump statements should not be redundant
            Should.NotThrow(() => worker.StartAsync());
        }
    }
}
