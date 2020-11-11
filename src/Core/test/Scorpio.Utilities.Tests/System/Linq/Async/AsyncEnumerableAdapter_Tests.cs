using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncEnumerableAdapter_Tests
    {
        [Fact]
        public async Threading.Tasks.Task GetCountAsync()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = new TestEnumerable<int>(array).ToAsyncEnumerable();
            (await (act as IAsyncIListProvider<int>).GetCountAsync(false, CancellationToken.None)).ShouldBe(8);
        }

        [Fact]
        public async Threading.Tasks.Task ToListAsync()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = new TestEnumerable<int>(array).ToAsyncEnumerable();
            var list = await (act as IAsyncIListProvider<int>).ToListAsync(CancellationToken.None);
            list.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Threading.Tasks.Task ToArrayAsync()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = new TestEnumerable<int>(array).ToAsyncEnumerable();
            var list = await (act as IAsyncIListProvider<int>).ToArrayAsync(CancellationToken.None);
            list.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_1()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = new TestEnumerable<int>(array).ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync(v => exp.Add(v));
            exp.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_2()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = new TestEnumerable<int>(array).ToAsyncEnumerable();
            var exp1 = new List<int>();
            var exp2 = new List<int>();
            await act.ForEachAsync(v => exp1.Add(v));
            await act.ForEachAsync(v => exp2.Add(v));
            exp1.SequenceEqual(exp2).ShouldBeTrue();
        }

    }

    internal class TestEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;

        public TestEnumerable(IEnumerable<T> source) => _source = source;
        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _source.GetEnumerator();
    }
}
