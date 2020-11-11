using System;

using Microsoft.AspNetCore.Http;

using NSubstitute;
using NSubstitute.Extensions;

using Scorpio.Auditing;

using Shouldly;

using Xunit;

namespace Scorpio.AspNetCore.Auditing
{
    public class AspNetCoreAuditContributor_Tests
    {
        [Fact]
        public void PreContribute_N()
        {
            var serviceProvider = Substitute.For<IServiceProvider>();
            var accessor = Substitute.For<IHttpContextAccessor>();
            accessor.HttpContext.Returns((HttpContext)null);
            serviceProvider.Configure().GetService(typeof(IHttpContextAccessor)).Returns(accessor);
            var context = new AuditContributionContext(serviceProvider, new AuditInfo());
            var contributor = new AspNetCoreAuditContributor();
            Should.NotThrow(() => contributor.PreContribute(context));
        }

        [Fact]
        public void PreContribute()
        {
            var serviceProvider = Substitute.For<IServiceProvider>();
            var accessor = Substitute.For<IHttpContextAccessor>();
            accessor.HttpContext.Returns(new DefaultHttpContext());
            serviceProvider.Configure().GetService(typeof(IHttpContextAccessor)).Returns(accessor);
            var context = new AuditContributionContext(serviceProvider, new AuditInfo());
            var contributor = new AspNetCoreAuditContributor();
            Should.NotThrow(() => contributor.PreContribute(context));
        }

    }
}
