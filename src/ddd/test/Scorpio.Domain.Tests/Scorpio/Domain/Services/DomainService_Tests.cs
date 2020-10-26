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
            var provider = Substitute.For<IServiceProvider>();
            var service = Substitute.ForPartsOf<DomainService>(provider);
            service.GetProperty("ServiceProvider").ShouldBe(provider);
        }
        [Fact]
        public void CurrentUnitOfWork()
        {
            var provider = Substitute.For<IServiceProvider>();
            var uowProvider = Substitute.For<ICurrentUnitOfWorkProvider>();
            var uow = Substitute.For<IUnitOfWork>();
            uowProvider.Current.Returns(uow);
            provider.GetService<ICurrentUnitOfWorkProvider>().Returns(uowProvider);
            var service = Substitute.ForPartsOf<DomainService>(provider);
            service.GetProperty("CurrentUnitOfWork").ShouldBe(uow);
        }


        [Fact]
        public void Null_Logger()
        {
            var provider = Substitute.For<IServiceProvider>();
            var service = Substitute.ForPartsOf<DomainService>(provider);
            service.GetProperty("Logger").ShouldBe(NullLogger.Instance);
        }


        [Fact]
        public void Logger()
        {
            var provider = Substitute.For<IServiceProvider>();
            var logger = Substitute.For<ILogger>();
            var factory = Substitute.For<ILoggerFactory>();
            factory.CreateLogger(typeof(DomainService)).ReturnsForAnyArgs(logger);
            provider.GetService<ILoggerFactory>().Returns(factory);
            var service = Substitute.ForPartsOf<DomainService>(provider);
            service.GetProperty("Logger").ShouldBe(logger);
        }
        [Fact]
        public void Clock()
        {
            var provider = Substitute.For<IServiceProvider>();
            var clock = Substitute.For<IClock>();
            provider.GetService<IClock>().Returns(clock);
            var service = Substitute.ForPartsOf<DomainService>(provider);
            service.GetProperty("Clock").ShouldBe(clock);
        }
    }
}
