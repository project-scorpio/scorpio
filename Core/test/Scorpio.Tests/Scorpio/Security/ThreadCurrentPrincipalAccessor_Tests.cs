using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Scorpio.Security;

using Shouldly;

using Xunit;

namespace Scorpio.Security
{
    public class ThreadCurrentPrincipalAccessor_Tests
    {
        [Fact]
        public void Principal()
        {
            new ThreadCurrentPrincipalAccessor().Principal.ShouldBe(Thread.CurrentPrincipal);
        }
    }
}
