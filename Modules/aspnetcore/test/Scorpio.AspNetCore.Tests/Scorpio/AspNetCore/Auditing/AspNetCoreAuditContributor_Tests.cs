using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            var contributor = new AspNetCoreAuditContributor(serviceProvider);
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
            var contributor = new AspNetCoreAuditContributor(serviceProvider);
            Should.NotThrow(() => contributor.PreContribute(context));
        }

    }
}
