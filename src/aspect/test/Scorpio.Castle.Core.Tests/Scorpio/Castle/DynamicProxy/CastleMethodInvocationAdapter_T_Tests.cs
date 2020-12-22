using System;
using System.Threading.Tasks;

using Castle.DynamicProxy;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Castle.DynamicProxy
{
    public class CastleMethodInvocationAdapter_T_Tests
    {
        [Fact]
        public void ProceedAsync()
        {
            var invocation = Substitute.For<IInvocation>();
            var processInfo = Substitute.For<IInvocationProceedInfo>();
            var proceed = Substitute.For<Func<IInvocation, IInvocationProceedInfo, Task<string>>>();
            var adapter = new CastleMethodInvocationAdapter<string>(invocation, processInfo, proceed);
            Should.NotThrow(() => adapter.ProceedAsync());
            proceed.Received(1)(invocation, processInfo);
        }
    }
}
