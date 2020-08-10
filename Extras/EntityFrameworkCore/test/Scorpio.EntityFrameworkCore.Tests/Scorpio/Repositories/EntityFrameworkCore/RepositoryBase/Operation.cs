
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public partial class RepositoryBase_Tests
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
        public void Update_Expression()
        {
            var repo = GetUsers();
           repo.GetDbContext().Entry ( repo.Insert(new TestTable { Id = 10, StringValue = "test" })).State= Microsoft.EntityFrameworkCore.EntityState.Detached;
            repo.Update(r => r.Id == 10, r => new TestTable { StringValue = "Jhon" });
            repo.Find(10).StringValue.ShouldBe("Jhon");
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync()
        {
            var repo = GetUsers();
            var entity = await repo.InsertAsync(new TestTable { Id = 10, StringValue = "test" });
            entity.StringValue = "Jhon";
            await repo.UpdateAsync(entity);
            repo.Find(10).StringValue.ShouldBe("Jhon");
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync_ExpressionAsync()
        {
            var repo = GetUsers();
            repo.GetDbContext().Entry(repo.Insert(new TestTable { Id = 10, StringValue = "test" })).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            await repo.UpdateAsync(r => r.Id == 10, r => new TestTable { StringValue = "Jhon" });
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
        public void Delete_Expression()
        {
            var repo = GetUsers();
            repo.GetDbContext().Entry(repo.Insert(new TestTable { Id = 10, StringValue = "test" })).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            repo.GetCount().ShouldBe(1);
            repo.Delete(r => r.Id == 12);
            repo.GetCount().ShouldBe(1);
            repo.Delete(r => r.Id == 10);
            repo.GetCount().ShouldBe(0);
            repo.IgnoreQueryFilters().Count().ShouldBe(1);
        }

        [Fact]
        public void Delete_Expression_S()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<SimpleTable, int>>();
            repo.GetDbContext().Entry(repo.Insert(new SimpleTable { Id = 10, StringValue = "test" })).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            repo.GetCount().ShouldBe(1);
            repo.Delete(r => r.Id == 12);
            repo.GetCount().ShouldBe(1);
            repo.Delete(r => r.Id == 10);
            repo.GetCount().ShouldBe(0);
            repo.IgnoreQueryFilters().Count().ShouldBe(0);
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
        public async System.Threading.Tasks.Task DeleteAsync_ExpressionAsync()
        {
            var repo = GetUsers();
            repo.GetDbContext().Entry(repo.Insert(new TestTable { Id = 10, StringValue = "test" })).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(r => r.Id == 12);
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(r => r.Id == 10);
            repo.GetCount().ShouldBe(0);
            repo.IgnoreQueryFilters().Count().ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync_ExpressionAsync_S()
        {
            var repo = ServiceProvider.GetRequiredService<IRepository<SimpleTable,int>>();
            repo.GetDbContext().Entry(repo.Insert(new SimpleTable  { Id = 10, StringValue = "test" })).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(r => r.Id == 12);
            repo.GetCount().ShouldBe(1);
            await repo.DeleteAsync(r => r.Id == 10);
            repo.GetCount().ShouldBe(0);
            repo.IgnoreQueryFilters().Count().ShouldBe(0);
        }
    }
}
