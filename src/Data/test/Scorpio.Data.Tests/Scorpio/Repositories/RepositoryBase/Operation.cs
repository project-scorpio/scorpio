
using Shouldly;

using Xunit;

namespace Scorpio.Repositories
{
    public partial class RepositoryBase_Tests
    {
        [Fact]
        public void Insert()
        {
            var (repo, list) = GetUsers();
            list.ShouldBeEmpty();
            repo.Insert(new User { Id = 10, Name = "Jhon" });
            list.ShouldHaveSingleItem().Id.ShouldBe(10);
        }
        [Fact]
        public async System.Threading.Tasks.Task InsertAsync()
        {
            var (repo, list) = GetUsers();
            list.ShouldBeEmpty();
            await repo.InsertAsync(new User { Id = 10, Name = "Jhon" });
            list.ShouldHaveSingleItem().Id.ShouldBe(10);
        }

        [Fact]
        public void Update()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Update(new User { Id = 10, Name = "Jhon" });
            list.ShouldHaveSingleItem().Name.ShouldBe("Jhon");
        }

        [Fact]
        public void Update_Expression()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Update(r => r.Id == 10, r => new User { Name = "Jhon" });
            list.ShouldHaveSingleItem().Name.ShouldBe("Jhon");
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            await repo.UpdateAsync(new User { Id = 10, Name = "Jhon" });
            list.ShouldHaveSingleItem().Name.ShouldBe("Jhon");
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync_ExpressionAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            await repo.UpdateAsync(r => r.Id == 10, r => new User { Name = "Jhon" });
            list.ShouldHaveSingleItem().Name.ShouldBe("Jhon");
        }

        [Fact]
        public void Delete()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Delete(list[0]);
            list.ShouldBeEmpty();
        }

        [Fact]
        public void Delete_Expression()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Delete(r => r.Id == 10);
            list.ShouldBeEmpty();
        }

        [Fact]
        public void Delete_ID()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Delete(12);
            list.ShouldHaveSingleItem();
            repo.Delete(10);
            list.ShouldBeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            await repo.DeleteAsync(list[0]);
            list.ShouldBeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync_ID()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            await repo.DeleteAsync(10);
            list.ShouldBeEmpty();
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync_ExpressionAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            await repo.DeleteAsync(r => r.Id == 10);
            list.ShouldBeEmpty();
        }
    }
}
