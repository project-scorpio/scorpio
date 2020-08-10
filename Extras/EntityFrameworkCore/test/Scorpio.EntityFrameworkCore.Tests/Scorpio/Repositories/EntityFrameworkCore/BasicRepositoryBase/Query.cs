using System;
using System.Linq;

using Scorpio.EntityFrameworkCore;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class BasicRepositoryBase_Tests
    {
        [Fact]
        public void GetList()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            var exp = repo.GetList();
            exp.ShouldHaveSingleItem().Id.ShouldBe(10);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetListAsync()
        {
            var repo = GetUsers();
            await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" });
            var exp = await repo.GetListAsync();
            exp.ShouldHaveSingleItem().Id.ShouldBe(10);
        }
        [Fact]
        public void GetCount()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            var exp = repo.GetCount();
            exp.ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCountAsync()
        {
            var repo = GetUsers();
            await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" });
            var exp = await repo.GetCountAsync();
            exp.ShouldBe(1);
        }
    }
}
