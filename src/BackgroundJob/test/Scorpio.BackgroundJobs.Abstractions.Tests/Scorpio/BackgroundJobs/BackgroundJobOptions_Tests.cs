using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class BackgroundJobOptions_Tests:BackgroundJobsAbstractionsTestBase
    {


        [Fact]
        public void GetJob()
        {
            var opt = ServiceProvider.GetService<IOptions<BackgroundJobOptions>>().Value;
            Should.NotThrow(() => opt.GetJob<string>())
                  .ShouldBeOfType<BackgroundJobConfiguration>()
                  .JobType.ShouldBe(typeof(FakeBackgroundJob));
            Should.Throw<ScorpioException>(() =>
            {
                opt.GetJob<int>();
            });
            Should.NotThrow(() => opt.GetJob(typeof(string).FullName))
                .ShouldBeOfType<BackgroundJobConfiguration>()
                .JobType.ShouldBe(typeof(FakeBackgroundJob));
            Should.Throw<ScorpioException>(() =>
            {
                opt.GetJob("test");
            });
            opt.GetJobs().ShouldHaveSingleItem().ArgsType.ShouldBe(typeof(string));
            opt.IsJobExecutionEnabled.ShouldBeTrue();
            opt.IsJobExecutionEnabled=false;
            opt.IsJobExecutionEnabled.ShouldBeFalse();
            opt.AddJob<FakeBackgroundJob>();
            opt.GetJobs().ShouldHaveSingleItem().ArgsType.ShouldBe(typeof(string));
        }


    }
}
