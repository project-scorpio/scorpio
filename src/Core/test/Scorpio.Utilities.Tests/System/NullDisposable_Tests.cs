
using System.Threading.Tasks;

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
    public class AsyncNullDisposable_Tests
    {
        [Fact]
        public async ValueTask Instance()
        {
            var instance = NullAsyncDispose.Instance;
          await  Should.NotThrow(() => instance.DisposeAsync());
        }
    }
}
