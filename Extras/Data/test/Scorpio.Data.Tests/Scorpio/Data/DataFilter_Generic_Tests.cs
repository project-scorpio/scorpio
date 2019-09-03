using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
namespace Scorpio.Data
{
    public class DataFilter_Generic_Tests
    {
        [Fact]
        public void Disable()
        {
            var filter = new DataFilter<ITestFilter>();
            filter.IsEnabled.ShouldBeTrue();
            using (filter.Disable())
            {
                filter.IsEnabled.ShouldBeFalse();
            }
            filter.IsEnabled.ShouldBeTrue();

            filter = new DataFilter<ITestFilter>(false);
            filter.IsEnabled.ShouldBeFalse();
            using (filter.Disable())
            {
                filter.IsEnabled.ShouldBeFalse();
            }
            filter.IsEnabled.ShouldBeFalse();
        }

        [Fact]
        public void Enable()
        {
            var filter = new DataFilter<ITestFilter>();
            filter.IsEnabled.ShouldBeTrue();
            using (filter.Enable())
            {
                filter.IsEnabled.ShouldBeTrue();
            }
            filter.IsEnabled.ShouldBeTrue();

            filter = new DataFilter<ITestFilter>(false);
            filter.IsEnabled.ShouldBeFalse();
            using (filter.Enable())
            {
                filter.IsEnabled.ShouldBeTrue();
            }
            filter.IsEnabled.ShouldBeFalse();
        }
    }
}
