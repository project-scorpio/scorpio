
using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    public class DatabaseFacadeExtensions_Tests : TestBase.IntegratedTest<TestModule>
    {
        [Fact]
        public void IsRelational()
        {
            using (var context = ServiceProvider.GetService<TestDbContext>())
            {
                context.Database.IsRelational().ShouldBeFalse();
            }

        }
    }
}
