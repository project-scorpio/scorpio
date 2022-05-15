using System.Threading;

using Shouldly;

using Xunit;

namespace Scorpio.Threading
{
    public class CancellationTokenProviderExtensions_Tests
    {
        [Fact]
        public void FallbackToProvider()
        {
            var source = new CancellationTokenSource();
            var act = new CancellationTokenSource().Token;
            var provider = new TestCancellationTokenProvider(source);
            provider.FallbackToProvider().ShouldBe(source.Token);
            provider.FallbackToProvider(CancellationToken.None).ShouldBe(source.Token);
            provider.FallbackToProvider(act).ShouldBe(act);
        }

        private class TestCancellationTokenProvider : ICancellationTokenProvider
        {
            private readonly CancellationTokenSource _source;

            public TestCancellationTokenProvider(CancellationTokenSource source) => _source = source;
            public CancellationToken Token => _source.Token;
        }
    }
}
