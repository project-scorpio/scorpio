using System.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NSubstitute;

using Scorpio.BackgroundWorkers;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class Module_Tests : BackgroundJobsTestBase
    {
        [Fact]
        public void Start()
        {
           ServiceProvider.GetService<IBackgroundWorkerManager>().ShouldBeOfType<BackgroundWorkerManager>().BackgroundWorkers.ShouldHaveSingleItem().ShouldBeOfType<BackgroundJobWorker>();
        }
    }
}
