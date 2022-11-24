
using Shouldly;

using Xunit;

namespace System
{
    public class NullDisposable_Tests
    {
        [Fact]
        public void Instance()
        {
            var instance = NullDisposable.Instance;
            Should.NotThrow(() => instance.Dispose());
        }
    }
    public class NullAsyncDisposable_Tests
    {
        [Fact]
        public void Instance()
        {
            var instance = NullAsyncDispose.Instance;
            Should.NotThrow(() => instance.DisposeAsync());
        }
    }
}
