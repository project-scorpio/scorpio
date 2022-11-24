
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore;
using Scorpio.Repositories;

using Shouldly;

using Xunit;
using System.Linq.Async;
namespace Scorpio.Linq
{
    public class EntityFrameworkCoreAsyncQueryableExecuter_Tests : EntityFrameworkCoreTestBase
    {
        [Fact]
        public void CountAsync()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            var executor = EntityFrameworkCoreAsyncQueryableExecuter.Instance;
            Should.NotThrow(() => executor.CountAsync(repo)).ShouldBe(0);
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => executor.CountAsync(repo)).ShouldBe(1);
        }


        [Fact]
        public void ToListAsync()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            var executor = EntityFrameworkCoreAsyncQueryableExecuter.Instance;
            Should.NotThrow(() => executor.ToListAsync(repo)).ShouldBeEmpty();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => executor.ToListAsync(repo)).ShouldHaveSingleItem();
            Should.NotThrow(() => repo.ToListAsync()).ShouldHaveSingleItem();
        }

        [Fact]
        public void FirstOrDefaultAsync()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            var executor = EntityFrameworkCoreAsyncQueryableExecuter.Instance;
            Should.NotThrow(() => executor.FirstOrDefaultAsync(repo)).ShouldBeNull();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => executor.FirstOrDefaultAsync(repo)).ShouldNotBeNull();
        }

        [Fact]
        public void AsAsyncEnumerable()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
            var executor = EntityFrameworkCoreAsyncQueryableExecuter.Instance;
            Should.NotThrow(() => executor.AsAsyncEnumerable(repo)).ShouldNotBeNull();
        }
    }
}
