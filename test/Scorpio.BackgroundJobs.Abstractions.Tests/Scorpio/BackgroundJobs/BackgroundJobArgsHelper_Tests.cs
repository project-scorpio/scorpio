
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class BackgroundJobArgsHelper_Tests
    {
        [Fact]
        public void GetJobArgsType()
        {
            Should.NotThrow(() => BackgroundJobArgsHelper.GetJobArgsType(typeof(AsyncBackgroundJob<string>))).ShouldBe(typeof(string));
            Should.NotThrow(() => BackgroundJobArgsHelper.GetJobArgsType(typeof(BackgroundJob<string>))).ShouldBe(typeof(string));
            Should.Throw<ScorpioException>(() => BackgroundJobArgsHelper.GetJobArgsType(typeof(DefaultServiceProviderFactory)));
            Should.Throw<ScorpioException>(() => BackgroundJobArgsHelper.GetJobArgsType(typeof(NullLogger<string>)));
        }
    }
}