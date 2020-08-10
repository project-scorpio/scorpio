using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

using Z.EntityFramework.Extensions;

namespace Scorpio.EntityFrameworkCore
{
    public class DefaultDbContextProvider_Tests:IntegratedTest<TestModule>
    {
        [Fact]
        public void GetDbContext()
        {
            var provider = ServiceProvider.GetService<IDbContextProvider<TestDbContext>>();
            provider.GetDbContext().ShouldNotBeNull();
        }
    }
}
