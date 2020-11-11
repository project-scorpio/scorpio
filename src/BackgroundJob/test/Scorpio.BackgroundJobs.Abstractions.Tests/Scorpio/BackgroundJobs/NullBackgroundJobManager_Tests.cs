
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class NullBackgroundJobManager_Tests : BackgroundJobsAbstractionsTestBase
    {
        [Fact]
        public void Test()
        {
            var manager = ServiceProvider.GetService<IBackgroundJobManager>().ShouldBeOfType<NullBackgroundJobManager>();
            manager.Logger.ShouldBeOfType<NullLogger<NullBackgroundJobManager>>();
            Should.Throw<ScorpioException>(() => manager.EnqueueAsync("Test"));
        }
    }
}
