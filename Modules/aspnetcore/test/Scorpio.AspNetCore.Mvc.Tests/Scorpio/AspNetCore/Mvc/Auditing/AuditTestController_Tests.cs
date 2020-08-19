using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using Scorpio.Auditing;

namespace Scorpio.AspNetCore.Mvc.Auditing
{
    public class AuditTestController_Tests : AspNetCoreMvcTestBase
    {
        private readonly AuditingOptions _options;
        private IAuditingStore _auditingStore;

        public AuditTestController_Tests()
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
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests()
        {
            await GetResponseAsync("api/audit-test/audit-success");
            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests_Disable()
        {
            _options.IsEnabled = false;
            await GetResponseAsync("api/audit-test/audit-success");
            await _auditingStore.Received(0).SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Success_For_GetRequests_Disable_Controller()
        {
            _options.DisableAuditingController();
            await GetResponseAsync("api/audit-test/audit-success");
            await _auditingStore.Received(0).SaveAsync(Arg.Any<AuditInfo>());
        }

        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Exception_Always()
        {
            _options.IsEnabled = true;

            try
            {
                await GetResponseAsync("api/audit-test/audit-fail", System.Net.HttpStatusCode.NotFound);
            }
            catch { }

            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());
        }
        
        [Fact]
        public async Task Should_Trigger_Middleware_And_AuditLog_Exception_When_Returns_Object()
        {
            _options.IsEnabled = true;

            await GetResponseAsync("api/audit-test/audit-fail-object", System.Net.HttpStatusCode.NotFound);

            await _auditingStore.Received().SaveAsync(Arg.Any<AuditInfo>());
        }
    }
}
