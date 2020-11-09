using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public  class BackgroundJob_Tests
    {
        [Fact]
        public void Logger()
        {
            var job=Substitute.ForPartsOf<BackgroundJob<string>>();
            job.Logger.ShouldBe(NullLogger<BackgroundJob<string>>.Instance);
            
        }


    }
}