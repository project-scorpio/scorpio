using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using Shouldly;

using Xunit;

namespace Scorpio.Linq
{
    public class DefaultAsyncQueryableExecuter_Tests
    {
        private readonly IQueryable<int> _sources = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.AsQueryable();

        [Fact]
        public async System.Threading.Tasks.Task CountAsync()
        {
            var executer = DefaultAsyncQueryableExecuter.Instance;
            (await executer.CountAsync(_sources)).ShouldBe(9);
        }

        [Fact]
        public async System.Threading.Tasks.Task FirstOrDefaultAsync()
        {
            var executer = DefaultAsyncQueryableExecuter.Instance;
            (await executer.FirstOrDefaultAsync(_sources)).ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task ToListAsync()
        {
            var executer = DefaultAsyncQueryableExecuter.Instance;
            (await executer.ToListAsync(_sources)).SequenceEqual(_sources).ShouldBeTrue();
        }

        [Fact]
        public void AsAsyncEnumerable()
        {
            var executer = DefaultAsyncQueryableExecuter.Instance;
            Should.Throw<InvalidOperationException>(() => executer.AsAsyncEnumerable(_sources));
            var mock = new Mock<ITestQueryable<string>>();
            var source = mock.Object;
            Should.NotThrow(() => executer.AsAsyncEnumerable(source));
        }


        public interface ITestQueryable<out T> : IQueryable<T>, IAsyncEnumerable<T>
        {

        }
    }
}
