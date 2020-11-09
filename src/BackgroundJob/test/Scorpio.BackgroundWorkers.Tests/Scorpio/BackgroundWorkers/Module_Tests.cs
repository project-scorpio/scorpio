using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundWorkers
{
    public class Module_Tests:BackgroundWorkersTestBase
    {
        private readonly IBackgroundWorker _worker;
        public Module_Tests()
        {
            _worker=Substitute.For<IBackgroundWorker>();
        }
        protected override void DisposeInternal(bool disposing)
        {
           _worker.ReceivedWithAnyArgs(1).StopAsync(Arg.Any<CancellationToken>());
        }
        [Fact]
        public void Start()
        {
            ServiceProvider.GetRequiredService<IOptions<BackgroundWorkerOptions>>().Value.IsEnabled.ShouldBeTrue();
            ServiceProvider.GetRequiredService<IBackgroundWorkerManager>().Add(_worker);
            _worker.ReceivedWithAnyArgs(1).StartAsync(Arg.Any<CancellationToken>());
        }
    }
}
