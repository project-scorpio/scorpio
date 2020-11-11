using System.Threading;

using Shouldly;

using Xunit;

namespace Scorpio.Threading
{
    public class NoneCancellationTokenProvider_Tests
    {
        [Fact]
        public void Instance() => NoneCancellationTokenProvider.Instance.ShouldNotBeNull();
        [Fact]
        public void Token() => NoneCancellationTokenProvider.Instance.Token.ShouldBe(CancellationToken.None);
    }
}
