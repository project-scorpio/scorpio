using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Scorpio.EntityFrameworkCore;
using Scorpio.Repositories;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncQueryable_Tests : EntityFrameworkCoreTestBase
    {


        private IRepository<TestTable, int> GetQueryable()
        {
            return ServiceProvider.GetRequiredService<IRepository<TestTable, int>>();
        }

        [Fact]
        public void AnyAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.AnyAsync()).ShouldBeFalse();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.AnyAsync()).ShouldBeTrue();
            Should.NotThrow(() => repo.AnyAsync(s => s.Id == 2)).ShouldBeFalse();
            Should.NotThrow(() => repo.AnyAsync(s => s.Id == 1)).ShouldBeTrue();
        }

        [Fact]
        public void AllAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 1)).ShouldBeTrue();
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 2)).ShouldBeTrue();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 1)).ShouldBeTrue();
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 2)).ShouldBeFalse();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 1)).ShouldBeFalse();
            Should.NotThrow(() => repo.AllAsync(s => s.Id == 2)).ShouldBeFalse();
            Should.NotThrow(() => repo.AllAsync(s => s.StringValue == "Jhon")).ShouldBeTrue();
        }



        [Fact]
        public void FirstAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.FirstAsync());
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.FirstAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => repo.FirstAsync(s => s.Id == 2));
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.FirstAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstAsync(s => s.StringValue == "Jhon")).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }
        [Fact]
        public void FirstOrDefaultAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.FirstOrDefaultAsync()).ShouldBeNull();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.FirstOrDefaultAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstOrDefaultAsync(s => s.Id == 2)).ShouldBeNull();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.FirstOrDefaultAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstOrDefaultAsync(s => s.StringValue == "Jhon")).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.FirstOrDefaultAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }

        [Fact]
        public void LastAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.LastAsync());
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LastAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.LastAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => repo.LastAsync(s => s.Id == 2));
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LastAsync()).ShouldNotBeNull().Id.ShouldBe(2);
            Should.NotThrow(() => repo.LastAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.LastAsync(s => s.StringValue == "Jhon")).ShouldNotBeNull().Id.ShouldBe(2);
            Should.NotThrow(() => repo.LastAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }

        [Fact]
        public void LastOrDefaultAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.LastOrDefaultAsync()).ShouldBeNull();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LastOrDefaultAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.LastOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.LastOrDefaultAsync(s => s.Id == 2)).ShouldBeNull();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LastOrDefaultAsync()).ShouldNotBeNull().Id.ShouldBe(2);
            Should.NotThrow(() => repo.LastOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.LastOrDefaultAsync(s => s.StringValue == "Jhon")).ShouldNotBeNull().Id.ShouldBe(2);
            Should.NotThrow(() => repo.LastOrDefaultAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }

        [Fact]
        public void CountAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.CountAsync()).ShouldBe(0);
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.CountAsync()).ShouldBe(1);
            Should.NotThrow(() => repo.CountAsync(s => s.Id == 1)).ShouldBe(1);
            Should.NotThrow(() => repo.CountAsync(s => s.Id == 2)).ShouldBe(0);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.CountAsync()).ShouldBe(2);
            Should.NotThrow(() => repo.CountAsync(s => s.Id == 1)).ShouldBe(1);
            Should.NotThrow(() => repo.CountAsync(s => s.Id == 2)).ShouldBe(1);
            Should.NotThrow(() => repo.CountAsync(s => s.StringValue == "Jhon")).ShouldBe(2);
        }

        [Fact]
        public void LongCountAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.LongCountAsync()).ShouldBe(0);
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LongCountAsync()).ShouldBe(1);
            Should.NotThrow(() => repo.LongCountAsync(s => s.Id == 1)).ShouldBe(1);
            Should.NotThrow(() => repo.LongCountAsync(s => s.Id == 2)).ShouldBe(0);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.LongCountAsync()).ShouldBe(2);
            Should.NotThrow(() => repo.LongCountAsync(s => s.Id == 1)).ShouldBe(1);
            Should.NotThrow(() => repo.LongCountAsync(s => s.Id == 2)).ShouldBe(1);
            Should.NotThrow(() => repo.LongCountAsync(s => s.StringValue == "Jhon")).ShouldBe(2);
        }

        [Fact]
        public void SingleAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.SingleAsync());
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.SingleAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.SingleAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => repo.SingleAsync(s => s.Id == 2));
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.Throw<InvalidOperationException>(() => repo.SingleAsync());
            Should.NotThrow(() => repo.SingleAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => repo.SingleAsync(s => s.StringValue == "Jhon"));
            Should.NotThrow(() => repo.SingleAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }

        [Fact]
        public void SingleOrDefaultAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.SingleOrDefaultAsync()).ShouldBeNull();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.SingleOrDefaultAsync()).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.SingleOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.NotThrow(() => repo.SingleOrDefaultAsync(s => s.Id == 2)).ShouldBeNull();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.Throw<InvalidOperationException>(() => repo.SingleOrDefaultAsync());
            Should.NotThrow(() => repo.SingleOrDefaultAsync(s => s.Id == 1)).ShouldNotBeNull().Id.ShouldBe(1);
            Should.Throw<InvalidOperationException>(() => repo.SingleAsync(s => s.StringValue == "Jhon"));
            Should.NotThrow(() => repo.SingleOrDefaultAsync(s => s.Id == 2)).ShouldNotBeNull().Id.ShouldBe(2);
        }

        [Fact]
        public void MinAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.MinAsync(s => s.Id));
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.MinAsync(s => s.Id)).ShouldBe(1);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.MinAsync(s => s.Id)).ShouldBe(1);
        }
        [Fact]
        public void MaxAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.MaxAsync(s => s.Id));
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.MaxAsync(s => s.Id)).ShouldBe(1);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.MaxAsync(s => s.Id)).ShouldBe(2);
        }
        [Fact]
        public void SumAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.SumAsync(s => s.Id)).ShouldBe(0);
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.SumAsync(s => s.Id)).ShouldBe(1);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.SumAsync(s => s.Id)).ShouldBe(3);

        }

        [Fact]
        public void AverageAsync()
        {
            var repo = GetQueryable();
            Should.Throw<InvalidOperationException>(() => repo.AverageAsync(s => s.Id));
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.AverageAsync(s => s.Id)).ShouldBe(1);
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.AverageAsync(s => s.Id)).ShouldBe(1.5);

        }

        [Fact]
        public void ContainsAsync()
        {
            var entity1 = new TestTable { Id = 1, StringValue = "Jhon" };
            var entity2 = new TestTable { Id = 2, StringValue = "Jhon" };
            var repo = GetQueryable();
            Should.NotThrow(() => repo.ContainsAsync(entity1)).ShouldBeFalse();
            repo.Insert(entity1);
            Should.NotThrow(() => repo.ContainsAsync(entity1)).ShouldBeTrue();
            Should.NotThrow(() => repo.ContainsAsync(entity2)).ShouldBeFalse();
            repo.Insert(entity2);
            Should.NotThrow(() => repo.ContainsAsync(entity1)).ShouldBeTrue();
            Should.NotThrow(() => repo.ContainsAsync(entity2)).ShouldBeTrue();

        }

        [Fact]
        public void ToArrayAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.ToArrayAsync()).ShouldBeOfType<TestTable[]>().ShouldBeEmpty();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToArrayAsync()).ShouldBeOfType<TestTable[]>().ShouldHaveSingleItem();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToArrayAsync()).ShouldBeOfType<TestTable[]>().ShouldBe(repo);
        }

        [Fact]
        public void ToListAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.ToListAsync()).ShouldBeOfType<List<TestTable>>().ShouldBeEmpty();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToListAsync()).ShouldBeOfType<List<TestTable>>().ShouldHaveSingleItem();
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToListAsync()).ShouldBeOfType<List<TestTable>>().ShouldBe(repo);
        }

        [Fact]
        public void ToDictionaryAsync()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id)).ShouldBeOfType<Dictionary<int, TestTable>>().ShouldBeEmpty();
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id, s => s.StringValue)).ShouldBeOfType<Dictionary<int, string>>().ShouldBeEmpty();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id)).ShouldBeOfType<Dictionary<int, TestTable>>().ShouldHaveSingleItem().Key.ShouldBe(1);
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id, s => s.StringValue)).ShouldBeOfType<Dictionary<int, string>>().ShouldHaveSingleItem().Value.ShouldBe("Jhon");
            repo.Insert(new TestTable { Id = 2, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id)).ShouldBeOfType<Dictionary<int, TestTable>>().Count.ShouldBe(2);
            Should.NotThrow(() => repo.ToDictionaryAsync(s => s.Id, s => s.StringValue)).ShouldBeOfType<Dictionary<int, string>>().Count.ShouldBe(2);
        }

        [Fact]
        public void ForEachAsync()
        {
            var repo = GetQueryable();
            var list = new List<int>();
            Should.NotThrow(() => repo.ForEachAsync(a => list.Add(a.Id)));
            list.ShouldBeEmpty();
            repo.Insert(new TestTable { Id = 1, StringValue = "Jhon" });
            Should.NotThrow(() => repo.ForEachAsync(a => list.Add(a.Id)));
            list.ShouldHaveSingleItem().ShouldBe(1);
        }

        [Fact]
        public void IAsyncEnumerable()
        {
            var repo = GetQueryable();
            Should.NotThrow(() => repo.AsAsyncEnumerable()).ShouldBeAssignableTo<IAsyncEnumerable<TestTable>>();
        }

    }
}
