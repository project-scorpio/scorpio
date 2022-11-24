using System.Threading;

using Shouldly;

using Xunit;

namespace Scorpio.Security
{
    public class ThreadCurrentPrincipalAccessor_Tests
    {
        [Fact]
        public void Principal() => new ThreadCurrentPrincipalAccessor().Principal.ShouldBe(Thread.CurrentPrincipal);
    }
}
