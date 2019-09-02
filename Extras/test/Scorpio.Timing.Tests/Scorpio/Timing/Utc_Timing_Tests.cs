using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Scorpio.Modularity;

namespace Scorpio.Timing
{
    public class Utc_Timing_Tests : IntegratedTest<UtcTimingModule>
    {
        [Fact]
        public void Now()
        {
            var clock = ServiceProvider.GetRequiredService<IClock>();
            clock.ShouldBeOfType<Clock>().ShouldNotBeNull();
            clock.Kind.ShouldBe(DateTimeKind.Utc);
            clock.Now.Kind.ShouldBe(DateTimeKind.Utc);
            clock.SupportsMultipleTimezone.ShouldBeTrue();
            var now = DateTime.Now;
            clock.Normalize(now).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(now).ShouldBe(now.ToUniversalTime());
            var unspecifiedNow = new DateTime(now.Ticks, DateTimeKind.Unspecified);
            clock.Normalize(unspecifiedNow).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(unspecifiedNow).ShouldBe(unspecifiedNow);
            var utcNow = DateTime.UtcNow;
            clock.Normalize(utcNow).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(utcNow).ShouldBe(utcNow);
        }
    }

    [DependsOn(typeof(TimingModule))]
    public class UtcTimingModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<ClockOptions>(c => c.Kind = DateTimeKind.Utc);
            base.ConfigureServices(context);
        }
    }
}
