using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio
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
}
