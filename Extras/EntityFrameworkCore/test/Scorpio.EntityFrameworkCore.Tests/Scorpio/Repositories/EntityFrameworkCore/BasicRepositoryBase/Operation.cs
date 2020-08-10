
using Scorpio.EntityFrameworkCore;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class BasicRepositoryBase_Tests
    {
        [Fact]
        public void Insert()
        {
            var repo = GetUsers();
            repo.GetCount().ShouldBe(0);
            repo.Insert(new TestTable { Id = 10, StringValue = "test" }, true);
            repo.GetCount().ShouldBe(1);
        }
        [Fact]
        public async System.Threading.Tasks.Task InsertAsync()
        {
            var repo = GetUsers();
            repo.GetCount().ShouldBe(0);
            await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" });
            repo.GetCount().ShouldBe(1);
        }

        [Fact]
        public void Update()
        {
            var repo = GetUsers();
            var entity = repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            entity.StringValue = "Jhon";
            repo.Update(entity);
            repo.Find(10).StringValue.ShouldBe("Jhon");
        }


        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync()
        {
            var repo = GetUsers();
            var entity =await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" });
            entity.StringValue = "Jhon";
           await repo.UpdateAsync(entity);
            repo.Find(10).StringValue.ShouldBe("Jhon");
        }


        [Fact]
        public void Delete()
        {
            var repo = GetUsers();
            var entity = new TestTable { Id = 10, StringValue = "test" };
            repo.Insert(entity);
            repo.GetCount().ShouldBe(1);
            repo.Delete(entity);
            repo.GetCount().ShouldBe(0);
        }

        [Fact]
        public void Delete_ID()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            repo.GetCount().ShouldBe(1);
            repo.Delete(12);
            repo.GetCount().ShouldBe(1);
            repo.Delete(10);
            repo.GetCount().ShouldBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync()
        {
            var repo = GetUsers();
            var entity = new TestTable { Id = 10, StringValue = "test" };
            await repo.InsertAsync(entity);
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(entity);
            repo.GetCount().ShouldBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync_ID()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" });
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(12);
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(10);
            repo.GetCount().ShouldBe(0);
        }

        [Fact]
        public void SaveChange()
        {
            var repo = GetUsers();
            repo.Insert(new TestTable { Id = 10, StringValue = "test" }, false);
            repo.GetCount().ShouldBe(0);
            Should.NotThrow(() => repo.SaveChanges());
            repo.GetCount().ShouldBe(1);
        }
        [Fact]
        public async System.Threading.Tasks.Task SaveChangeAsync()
        {
            var repo = GetUsers();
           await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" }, false);
            repo.GetCount().ShouldBe(0);
            Should.NotThrow(() => repo.SaveChangesAsync());
            repo.GetCount().ShouldBe(1);
        }

    }
}
