
using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobNameAttribute_Tests
    {

        [Fact]
        public void GetName()
        {
            BackgroundJobNameAttribute.GetName<string>().ShouldBe(typeof(string).FullName);
            BackgroundJobNameAttribute.GetName<FakeBackgroundJobArg>().ShouldBe("Fake");
        }


        [BackgroundJobName("Fake")]
        private class FakeBackgroundJobArg
        {

        }
    }
}
