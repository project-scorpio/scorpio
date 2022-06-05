using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NSubstitute;

using Scorpio.Auditing;

using Xunit;

namespace Scorpio.AspNetCore.Mvc.Auditing
{
    public class AuditTestPage_Tests : AspNetCoreMvcTestBase
    {
        private readonly AuditingOptions _options;
        private IAuditingStore _auditingStore;

        public AuditTestPage_Tests()
        {
            _options = ServiceProvider.GetRequiredService<IOptions<AuditingOptions>>().Value;
            _auditingStore = ServiceProvider.GetRequiredService<IAuditingStore>();
        }

        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.ConfigureServices(c =>
            {
                _auditingStore = Substitute.For<IAuditingStore>();
                c.Services.ReplaceOrAdd(ServiceDescriptor.Singleton(_auditingStore), true);
            });
            base.SetBootstrapperCreationOptions(options);
        }


        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests()
        {
            await GetResponseAsync("/AuditTestPage?handler=AuditSuccessForGetRequests");
            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests_Disable()
        {
            _options.IsEnabled = false;
            await GetResponseAsync("/AuditTestPage?handler=AuditSuccessForGetRequests");
            await _auditingStore.Received(0).SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests_DisablePage()
        {
            _options.DisableAuditingPage();
            await GetResponseAsync("/AuditTestPage?handler=AuditSuccessForGetRequests");
            await _auditingStore.Received(0).SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Exception_Always()
        {
            _options.IsEnabled = true;

            await GetResponseAsync("/AuditTestPage?handler=AuditFailForGetRequests", System.Net.HttpStatusCode.NotFound);
            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());

        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Exception_When_Returns_Object()
        {
            _options.IsEnabled = true;

            await GetResponseAsync("/AuditTestPage?handler=AuditFailForGetRequestsReturningObject", System.Net.HttpStatusCode.NotFound);

            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());
        }
    }
}
