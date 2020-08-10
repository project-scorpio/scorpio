
using Scorpio.Entities;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class BasicRepositoryBase_Tests
    {
        [Fact]
        public void Find()
        {
            var repo = GetUsers();
            repo.Insert(new Scorpio.EntityFrameworkCore.TestTable { Id = 10, StringValue = "test" });
            repo.Find(12).ShouldBeNull();
            repo.Find(10).ShouldNotBeNull();
        }

        [Fact]
        public async System.Threading.Tasks.Task FindAsync()
        {
            var repo = GetUsers();
            repo.Insert(new Scorpio.EntityFrameworkCore.TestTable { Id = 10, StringValue = "test" });
            (await repo.FindAsync(12)).ShouldBeNull();
            (await repo.FindAsync(10)).ShouldNotBeNull();
        }

        [Fact]
        public void Get()
        {
            var repo = GetUsers();
            repo.Insert(new Scorpio.EntityFrameworkCore.TestTable { Id = 10, StringValue = "test" });
            Should.Throw<EntityNotFoundException>(() => repo.Get(12));
            Should.NotThrow(() => repo.Get(10)).ShouldNotBeNull();
        }

        [Fact]
        public void GetAsync()
        {
            var repo = GetUsers();
            repo.Insert(new Scorpio.EntityFrameworkCore.TestTable { Id = 10, StringValue = "test" });
            Should.Throw<EntityNotFoundException>(() => repo.GetAsync(12));
            Should.NotThrow(() => repo.GetAsync(10)).ShouldNotBeNull();
        }

    }
}
