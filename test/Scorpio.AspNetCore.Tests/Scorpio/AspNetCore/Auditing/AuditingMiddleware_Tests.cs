
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using NSubstitute;

using Scorpio.Auditing;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Auditing
{
    public class AuditingMiddleware_Tests
    {
        [Fact]
        public void Invoke()
        {
            var next = Substitute.For<RequestDelegate>();
            var manager = Substitute.For<IAuditingManager>();
            var handle = Substitute.For<IAuditSaveHandle>();
            manager.BeginScope().Returns(handle);
            var options = new AuditingOptions();
            var middleware = new AuditingMiddleware(next, manager, new OptionsWrapper<AuditingOptions>(options));
            Should.NotThrow(() => middleware.Invoke(new DefaultHttpContext()));
            handle.ReceivedWithAnyArgs(1).SaveAsync();
            options.IsEnabled = false;
            handle.ClearReceivedCalls();
            Should.NotThrow(() => middleware.Invoke(new DefaultHttpContext()));
            handle.ReceivedWithAnyArgs(0).SaveAsync();
            options.IsEnabled = true;
            options.IsEnabledForAnonymousUsers = false;
            handle.ClearReceivedCalls();
            Should.NotThrow(() => middleware.Invoke(new DefaultHttpContext()));
            handle.ReceivedWithAnyArgs(0).SaveAsync();

        }
    }
}
