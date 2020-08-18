using NSubstitute;

using Scorpio.AspNetCore.Auditing;
using Scorpio.AspNetCore.Uow;

using Xunit;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public  class ApplicationBuilderExtensions_Tests
    {

        [Fact]
        public void UseAuditing()
        {
            var builder = Substitute.For<IApplicationBuilder>();
            builder.UseAuditing();
            builder.Received(1).UseMiddleware<AuditingMiddleware>();
        }

        [Fact]
        public void UseUnitOfWork()
        {
            var builder = Substitute.For<IApplicationBuilder>();
            builder.UseUnitOfWork();
            builder.Received(1).UseMiddleware<UnitOfWorkMiddleware>();
        }

    }
}
