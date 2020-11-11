using System;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using Scorpio.Timing;
using Scorpio.Uow;

using Shouldly;

using Xunit;

namespace Scorpio.Domain.Services
{
    public class DomainService_Tests
    {
        [Fact]
        public void ServiceProvider()
        {
            var service = Substitute.ForPartsOf<DomainService>();
            service.GetProperty("ServiceProvider").ShouldBe(null);
        }
        [Fact]
        public void CurrentUnitOfWork()
        {
            var service = Substitute.ForPartsOf<DomainService>();
            service.GetProperty("CurrentUnitOfWork").ShouldBe(null);
        }


        [Fact]
        public void Null_Logger()
        {
            var service = Substitute.ForPartsOf<DomainService>();
            service.GetProperty("Logger").ShouldBe(NullLogger<DomainService>.Instance);
        }


        [Fact]
        public void Clock()
        {
            var service = Substitute.ForPartsOf<DomainService>();
            service.GetProperty("Clock").ShouldBe(null);
        }
    }
}
