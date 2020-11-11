using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.EntityFrameworkCore;
using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    public class RepositoryExtensions_Tests : EntityFrameworkCoreTestBase
    {

        [Fact]
        public void GetDbContext()
        {
            var repo = ServiceProvider.GetRequiredService<IBasicRepository<TestTable, int>>();
            repo.GetDbContext().ShouldBeOfType<TestDbContext>().ShouldNotBeNull();
        }

        [Fact]
        public void ToEfCoreRepository()
        {
            var repo = ServiceProvider.GetRequiredService<IBasicRepository<TestTable, int>>();
            repo.ToEfCoreRepository().ShouldBeOfType<EfCoreRepository<TestDbContext, TestTable, int>>().ShouldNotBeNull();
        }

        [Fact]
        public void ToEfCoreRepository_E()
        {
            var repo = Substitute.For<IRepository<TestTable, int>>();
            Should.Throw<ArgumentException>(() => repo.ToEfCoreRepository());
        }

        [Fact]
        public void EnsureCollectionLoaded()
        {
            var repo = ServiceProvider.GetRequiredService<IBasicRepository<TestTable, int>>();
            var entity = repo.Insert(new TestTable
            {
                Id = 10,
                StringValue = "test",
                Details = new HashSet<TestTableDetail>
                {
                    new TestTableDetail{ Id=1, DetailValue="Test"}
                }
            });

            repo.EnsureCollectionLoaded(entity, e => e.Details);
            entity.Details.Count.ShouldBe(1);
        }

        [Fact]
        public void EnsurePropertyLoaded()
        {
            var repo = ServiceProvider.GetRequiredService<IBasicRepository<TestTableDetail, int>>();
            repo.Insert(new TestTableDetail
            {
                Id = 10,
                DetailValue = "test",
                TestTable = new TestTable { Id = 1, StringValue = "Test" }
            });
            var entity = repo.Get(10);
            repo.EnsurePropertyLoaded(entity, e => e.TestTable);
            entity.TestTable.ShouldNotBeNull();
        }


    }
}
