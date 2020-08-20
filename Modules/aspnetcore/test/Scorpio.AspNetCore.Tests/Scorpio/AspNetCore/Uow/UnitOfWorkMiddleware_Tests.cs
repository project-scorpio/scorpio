using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using Scorpio.Uow;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Uow
{
    public class UnitOfWorkMiddleware_Tests
    {
        [Fact]
        public void Invoke()
        {
            var next = Substitute.For<RequestDelegate>();
            var manager = Substitute.For<IUnitOfWorkManager>();
            var uow = Substitute.For<IUnitOfWorkCompleteHandle>();
            manager.Begin().Returns(uow);
            var middleware = new UnitOfWorkMiddleware(next, manager);
            Should.NotThrow(() => middleware.Invoke(new DefaultHttpContext()));
            uow.ReceivedWithAnyArgs(1).CompleteAsync(default);
        }
    }
}
