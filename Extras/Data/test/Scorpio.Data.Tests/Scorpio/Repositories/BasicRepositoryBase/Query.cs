using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories
{
    public partial class BasicRepositoryBase_Tests
    {
        [Fact]
        public void GetList()
        {
            var (repo, list) = GetUsers();
            repo.Insert(new User { Id = 10, Name = "Jhon" });
            var exp = repo.GetList();
            exp.SequenceEqual(list).ShouldBeTrue();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetListAsync()
        {
            var (repo, list) = GetUsers();
            repo.Insert(new User { Id = 10, Name = "Jhon" });
            var exp = await repo.GetListAsync();
            exp.SequenceEqual(list).ShouldBeTrue();
        }
        [Fact]
        public void GetCount()
        {
            var (repo, list) = GetUsers();
            repo.Insert(new User { Id = 10, Name = "Jhon" });
            var exp = repo.GetCount();
            exp.ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCountAsync()
        {
            var (repo, list) = GetUsers();
            repo.Insert(new User { Id = 10, Name = "Jhon" });
            var exp = await repo.GetCountAsync();
            exp.ShouldBe(1);
        }
    }
}
