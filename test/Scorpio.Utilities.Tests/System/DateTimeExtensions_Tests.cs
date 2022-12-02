using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace System
{
    public class DateTimeExtensions_Tests
    {
        [Fact]
        public void ToUnixTimestamp()
        {
            var dateTime = DateTime.Now;
            var except = new DateTimeOffset(dateTime).ToUnixTimeSeconds();
            dateTime.ToUnixTimestamp().ShouldBe(except);
        }
    }
}
