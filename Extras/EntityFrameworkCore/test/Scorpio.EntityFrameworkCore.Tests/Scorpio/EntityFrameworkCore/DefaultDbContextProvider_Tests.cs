
using Microsoft.Extensions.DependencyInjection;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    public class DefaultDbContextProvider_Tests : IntegratedTest<TestModule>
    {
        [Fact]
        public void GetDbContext()
        {
            var provider = ServiceProvider.GetService<IDbContextProvider<TestDbContext>>();
            provider.GetDbContext().ShouldNotBeNull();
        }
    }
}
