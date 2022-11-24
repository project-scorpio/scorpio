
using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class BackgroundJobConfiguration_Tests
    {
        [Fact]
        public void Default()
        {
            var config = new BackgroundJobConfiguration(typeof(BackgroundJob<string>));
            config.ArgsType.ShouldBe(typeof(string));
            config.JobType.ShouldBe(typeof(BackgroundJob<string>));
            config.JobName.ShouldBe(typeof(string).FullName);
        }
    }
}