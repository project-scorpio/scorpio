using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Provides the abstract base class for a asynchronous background job.
    /// </summary>
    /// <typeparam name="TArgs">The args for the background job execution.</typeparam>
    public  class AsyncBackgroundJob_Tests
    {

    
       [Fact]
        public void Logger()
        {
            var job=Substitute.ForPartsOf<AsyncBackgroundJob<string>>();
            job.Logger.ShouldBe(NullLogger<AsyncBackgroundJob<string>>.Instance);
        }
    }
}