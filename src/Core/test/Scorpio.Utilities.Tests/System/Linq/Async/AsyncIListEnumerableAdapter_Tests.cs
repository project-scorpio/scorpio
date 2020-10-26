using System.Collections.Generic;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncIListEnumerableAdapter_Tests
    {
        [Fact]
        public void IndexOf()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = act as IList<int>;
            list.ShouldNotBeNull();
            list.IndexOf(7).ShouldBe(6);
        }

        [Fact]
        public void Insert()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = act as IList<int>;
            list.ShouldNotBeNull();
            list.Insert(0, 9);
            array.Count.ShouldBe(9);
            array.First().ShouldBe(9);
        }

        [Fact]
        public void RemoveAt()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = act as IList<int>;
            list.ShouldNotBeNull();
            list.RemoveAt(0);
            array.Count.ShouldBe(7);
            array.First().ShouldBe(2);
        }

        [Fact]
        public void This()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var list = act as IList<int>;
            list.ShouldNotBeNull();
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = 8 - i;
            }
            array.First().ShouldBe(8);
            array.Last().ShouldBe(1);
        }


        [Fact]
        public async Task ForEachAsync_1()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var exp = new List<int>();
            await act.ForEachAsync(v => exp.Add(v));
            exp.SequenceEqual(array).ShouldBeTrue();
        }

        [Fact]
        public async Task ForEachAsync_2()
        {
            var array = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var act = array.ToAsyncEnumerable();
            var exp1 = new List<int>();
            var exp2 = new List<int>();
            await act.ForEachAsync(v => exp1.Add(v));
            await act.ForEachAsync(v => exp2.Add(v));
            exp1.SequenceEqual(exp2).ShouldBeTrue();
        }
    }
}
