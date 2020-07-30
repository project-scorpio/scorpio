using System;
using System.Collections.Generic;
using System.Text;

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
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Local });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);
            var date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).Day.ShouldBe(31);
            clock.SupportsMultipleTimezone.ShouldBeFalse();
        }

        [Fact]
        public void Unspecified()
        {
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Unspecified });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Local);
            var date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Local);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Unspecified);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 23, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).Day.ShouldBe(30);
            clock.SupportsMultipleTimezone.ShouldBeFalse();
        }

        [Fact]
        public void Utc()
        {
            var options = new OptionsWrapper<ClockOptions>(new ClockOptions { Kind = DateTimeKind.Utc });
            var clock = new Clock(options);
            clock.Now.Kind.ShouldBe(DateTimeKind.Utc);
            var date = new DateTime(2020, 07, 30, 1, 0, 0, DateTimeKind.Local);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).Day.ShouldBe(29);
            date = new DateTime(2020, 07, 30, 1, 0, 0, DateTimeKind.Unspecified);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).Day.ShouldBe(30);
            date = new DateTime(2020, 07, 30, 1, 0, 0, DateTimeKind.Utc);
            clock.Normalize(date).Kind.ShouldBe(DateTimeKind.Utc);
            clock.Normalize(date).Day.ShouldBe(30);
            clock.SupportsMultipleTimezone.ShouldBeTrue();
        }

    }
}
