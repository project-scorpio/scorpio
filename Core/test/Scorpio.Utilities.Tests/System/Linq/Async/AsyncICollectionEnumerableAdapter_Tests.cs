using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncICollectionEnumerableAdapter_Tests
    {
        [Fact]
        public async Threading.Tasks.Task GetCountAsync()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (await (act as IAsyncIListProvider<int>).GetCountAsync(false, CancellationToken.None)).ShouldBe(8);
        }

        [Fact]
        public async Threading.Tasks.Task ToListAsync()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = await (act as IAsyncIListProvider<int>).ToListAsync(CancellationToken.None);
            list.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Threading.Tasks.Task ToArrayAsync()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = await (act as IAsyncIListProvider<int>).ToArrayAsync(CancellationToken.None);
            list.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public void IsReadOnly()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (act as ICollection<int>).IsReadOnly.ShouldBeFalse();
        }

        [Fact]
        public void Add()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (act as ICollection<int>).Add(9);
            array.Count.ShouldBe(9);
            array.Last().ShouldBe(9);
        }

        [Fact]
        public void Clear()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (act as ICollection<int>).Clear();
            array.ShouldBeEmpty();
        }

        [Fact]
        public void Contains()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (act as ICollection<int>).Contains(8).ShouldBeTrue();
        }

        [Fact]
        public void CopyTo()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var exp = new int[8];
            (act as ICollection<int>).CopyTo(exp, 0);
            array.SequenceEqual(exp).ShouldBeTrue();
        }

        [Fact]
        public void Remove()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            (act as ICollection<int>).Remove(8).ShouldBeTrue();
            array.Count.ShouldBe(7);
            array.Last().ShouldBe(7);
        }


        [Fact]
        public void ForEach_1()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var exp = new List<int>();
            (act as ICollection<int>).ForEach(v => exp.Add(v));
            exp.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_2()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var exp1 = new List<int>();
            var exp2 = new List<int>();
            await act.ForEachAsync(v => exp1.Add(v));
            await act.ForEachAsync(v => exp2.Add(v));
            exp1.SequenceEqual(exp2).ShouldBeTrue();
        }

        [Fact]
        public void GetEnumerator()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var enumerator = (act as IEnumerable).GetEnumerator();
            enumerator.MoveNext().ShouldBeTrue();
        }

        [Fact]
        public async Task GetExceptionEnumeratorAsync()
        {
            var array = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var enumerator = (act).GetAsyncEnumerator();
            (await enumerator.MoveNextAsync()).ShouldBeTrue();
            array.Add(9);
            Should.Throw<InvalidOperationException>(async () => (await enumerator.MoveNextAsync()).ShouldBeTrue());
        }

    }
}
