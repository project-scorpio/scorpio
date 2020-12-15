using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Castle.DynamicProxy
{
    public class CastleMethodInvocationAdapter_Tests
    {
        [Fact]
        public void ProceedAsync()
        {
            var invocation = Substitute.For<IInvocation>();
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            var proceed = Substitute.For<Func<IInvocation, IInvocationProceedInfo, Task>>();
            var adapter = new CastleMethodInvocationAdapter(invocation, processInfo, proceed);
            Should.NotThrow(() => adapter.ProceedAsync());
            proceed.Received(1)(invocation, processInfo);
        }
    }
}
