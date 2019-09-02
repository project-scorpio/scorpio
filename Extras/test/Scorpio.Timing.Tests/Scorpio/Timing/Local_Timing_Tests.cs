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
    public class Local_Timing_Tests : IntegratedTest<LocalTimingModule>
    {
        [Fact]
        public void Now()
        {
            var clock = ServiceProvider.GetRequiredService<IClock>();
            clock.ShouldBeOfType<Clock>().ShouldNotBeNull();
            clock.Kind.ShouldBe(DateTimeKind.Local);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);

            clock.SupportsMultipleTimezone.ShouldBeFalse();
            var now = DateTime.Now;
            var unspecifiedNow = new DateTime(now.Ticks, DateTimeKind.Unspecified);
            clock.Normalize(now).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(now).ShouldBe(now);
            clock.Normalize(unspecifiedNow).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(unspecifiedNow).ShouldBe(unspecifiedNow);
            var utcNow = DateTime.UtcNow;
            clock.Normalize(utcNow).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(utcNow).ShouldBe(utcNow.ToLocalTime());
        }
    }

    [DependsOn(typeof(TimingModule))]
    public class LocalTimingModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<ClockOptions>(c => c.Kind = DateTimeKind.Local);
            base.ConfigureServices(context);
        }
    }
}
