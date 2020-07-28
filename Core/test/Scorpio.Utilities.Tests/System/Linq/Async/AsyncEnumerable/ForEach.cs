using System.Collections.Generic;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public partial class AsyncEnumerable_Tests
    {
        [Fact]
        public async Task ForEachAsync_1()
        {
            var act = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync(v => exp.Add(v));
            exp.SequenceEqual(act as IList<int>).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_2()
        {
            var datas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = datas.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync((v, i) => exp.Add(v));
            datas.SequenceEqual(exp).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_3()
        {
            var datas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = datas.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync(v => Task.Run(() => exp.Add(v)));
            datas.SequenceEqual(exp).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_4()
        {
            var datas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = datas.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync((v, c) => Task.Run(() => exp.Add(v)));
            datas.SequenceEqual(exp).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_5()
        {
            var datas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = datas.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync((v, i, c) => Task.Run(() => exp.Add(v)));
            datas.SequenceEqual(exp).ShouldBeTrue();
        }

        [Fact]
        public async Task GetAsyncEnumerator()
        {
            var datas = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = datas.ToAsyncEnumerable();
            var enumerator = act.GetAsyncEnumerator();
            (await enumerator.MoveNextAsync()).ShouldBeTrue();
            await enumerator.DisposeAsync();
            (await enumerator.MoveNextAsync()).ShouldBeFalse();
        }

    }
}
