using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
namespace Scorpio.Timing
{
    public class Unspecified_Timing_Tests : IntegratedTest<TimingModule>
    {

        [Fact]
        public void Now()
        {
            var clock = ServiceProvider.GetRequiredService<IClock>();
            clock.ShouldBeOfType<Clock>().ShouldNotBeNull();
            clock.Kind.ShouldBe(DateTimeKind.Unspecified);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);
            clock.SupportsMultipleTimezone.ShouldBeFalse();
            var now = DateTime.Now.ToLocalTime();
            clock.Normalize(now).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(now).ShouldBe(now);
            var utcNow = DateTime.UtcNow;
            clock.Normalize(utcNow).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(utcNow).ShouldBe(utcNow);
        }
    }
}
