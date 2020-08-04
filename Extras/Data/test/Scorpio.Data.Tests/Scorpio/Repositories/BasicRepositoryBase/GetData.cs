using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Scorpio.Entities;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories
{
    public partial class BasicRepositoryBase_Tests
    {
        [Fact]
        public void Find()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            repo.Find(12).ShouldBeNull();
            repo.Find(10).ShouldNotBeNull();
        }

        [Fact]
        public async System.Threading.Tasks.Task FindAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            (await repo.FindAsync(12)).ShouldBeNull();
            (await repo.FindAsync(10)).ShouldNotBeNull();
        }

        [Fact]
        public void Get()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            Should.Throw<EntityNotFoundException>(() => repo.Get(12));
            Should.NotThrow(() => repo.Get(10)).ShouldNotBeNull();
        }

        [Fact]
        public void GetAsync()
        {
            var (repo, list) = GetUsers();
            list.Add(new User { Id = 10, Name = "Tom" });
            Should.Throw<EntityNotFoundException>(() => repo.GetAsync(12));
            Should.NotThrow(() => repo.GetAsync(10)).ShouldNotBeNull();
        }

    }
}
