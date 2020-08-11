
using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.EntityFrameworkCore
{
    public class DbContextExtensions_Tests : TestBase.IntegratedTest<TestModule>
    {
        [Fact]
        public void HasRelationalTransactionManager()
        {
            using (var context = ServiceProvider.GetService<TestDbContext>())
            {
                context.HasRelationalTransactionManager().ShouldBeFalse();
            }

        }
    }
}
