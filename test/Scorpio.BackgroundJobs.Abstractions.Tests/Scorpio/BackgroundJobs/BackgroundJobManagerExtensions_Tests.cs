
using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// Some extension methods for <see cref="IBackgroundJobManager"/>.
    /// </summary>
    public class BackgroundJobManagerExtensions_Tests
    {
        [Fact]
        public void IsAvailable()
        {
            new NullBackgroundJobManager().IsAvailable().ShouldBeFalse();
            Substitute.For<IBackgroundJobManager>().IsAvailable().ShouldBeTrue();
        }
    }
}
