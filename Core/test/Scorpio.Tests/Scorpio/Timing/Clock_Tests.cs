using System;

using Microsoft.Extensions.Options;

using Shouldly;

using Xunit;

namespace Scorpio.Timing
{
    public class Clock_Tests
    {
        [Fact]
        public void Local()
        {
            var offset = TimeZoneInfo.Local.BaseUtcOffset.Hours;
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Local });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);
            var date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).ShouldBe(date);
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).ShouldBe(date.AddHours(offset));
            clock.SupportsMultipleTimezone.ShouldBeFalse();
        }

        [Fact]
        public void Unspecified()
        {
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Unspecified });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);
            var date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).ShouldBe(date);
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Unspecified);
            clock.Normalize(date).ShouldBe(date);
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).ShouldBe(date);
            clock.SupportsMultipleTimezone.ShouldBeFalse();
        }

        [Fact]
        public void Utc()
        {
            var offset = TimeZoneInfo.Local.BaseUtcOffset.Hours;
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Utc });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Utc);
            var date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).ShouldBe(date.AddHours(-offset));
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).ShouldBe(date);
            date = new DateTime(2020, 07, 30, 0, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).ShouldBe(date);
            clock.SupportsMultipleTimezone.ShouldBeTrue();
        }

    }
}
