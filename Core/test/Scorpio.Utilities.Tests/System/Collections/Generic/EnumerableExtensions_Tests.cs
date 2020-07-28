using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Collections.Generic
{
    public class EnumerableExtensions_Tests
    {
        [Fact]
        public void IsNullOrEmpty()
        {
            IEnumerable<int> enumerable = null;
            enumerable.IsNullOrEmpty().ShouldBeTrue();
            enumerable = new List<int>();
            enumerable.IsNullOrEmpty().ShouldBeTrue();
            (enumerable as List<int>).Add(10);
            enumerable.IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void ExpandToString()
        {
            var enumerable = new List<int> { 4, 5, 6 };
            enumerable.ExpandToString(",").ShouldBe("4,5,6");
        }
        [Fact]
        public void ExpandToStringAsString()
        {
            var enumerable = new List<string> { "a", "bc", "d" };
            enumerable.ExpandToString(",").ShouldBe("a,bc,d");
        }

        [Fact]
        public void WhereIf()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 4, 5, 5, 5, 6, 6, 7, 8, 8, 8, 9 };
            enumerable.WhereIf(true, f => f == 4).Count().ShouldBe(2);
            enumerable.WhereIf(false, f => f == 4).Count().ShouldBe(13);
            enumerable.WhereIf(true, (f, i) => i == 8).ShouldHaveSingleItem().ShouldBe(7);
            enumerable.WhereIf(true, (f, i) => f == 4).Count().ShouldBe(2);
            enumerable.WhereIf(false, (f, i) => i == 8).Count().ShouldBe(13);
            enumerable.WhereIf(false, (f, i) => f == 4).Count().ShouldBe(13);
        }

        [Fact]
        public void ForEach()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            enumerable.ForEach(i => visitedItems.Add(i));
            visitedItems.SequenceEqual(enumerable).ShouldBeTrue();
        }

        [Fact]
        public void ForEachAsync_11()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            enumerable.ForEachAsync(i => { visitedItems.Add(i); return Task.CompletedTask; }).Wait();
            visitedItems.SequenceEqual(enumerable).ShouldBeTrue();
        }

        [Fact]
        public void ForEachAsync_12()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            enumerable.ForEachAsync((i, c) => { visitedItems.Add(i); return Task.CompletedTask; }).Wait();
            visitedItems.SequenceEqual(enumerable).ShouldBeTrue();
        }


        [Fact]
        public void ForEachAsync_21()
        {
            IEnumerable<int> enumerable = null;
            var visitedItems = new List<int>();
            enumerable.ForEachAsync(i => { visitedItems.Add(i); return Task.CompletedTask; }).Wait();
            visitedItems.ShouldBeEmpty();
        }

        [Fact]
        public void ForEachAsync_22()
        {
            IEnumerable<int> enumerable = null;
            var visitedItems = new List<int>();
            enumerable.ForEachAsync((Func<int, Task>)null).Wait();
            visitedItems.ShouldBeEmpty();
        }


        [Fact]
        public void ForEachAsync_31()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            enumerable.ForEachAsync((Func<int, Task>)null).Wait();
            visitedItems.ShouldBeEmpty();
        }

        [Fact]
        public void ForEachAsync_32()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            enumerable.ForEachAsync((Func<int, CancellationToken, Task>)null).Wait();
            visitedItems.ShouldBeEmpty();
        }

        [Fact]
        public void ForEachAsync_41()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            var cs = new CancellationTokenSource();
            enumerable.ForEachAsync(i =>
            {
                visitedItems.Add(i);
                if (i == 4)
                {
                    cs.Cancel();
                }
                return Task.CompletedTask;
            }, cs.Token).Wait();
            visitedItems.SequenceEqual(enumerable).ShouldBeFalse();
            visitedItems.ShouldContain(3);
            visitedItems.ShouldContain(4);
            visitedItems.ShouldNotContain(5);
        }

        [Fact]
        public void ForEachAsync_42()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            var visitedItems = new List<int>();
            var cs = new CancellationTokenSource();
            enumerable.ForEachAsync((i, c) =>
            {
                visitedItems.Add(i);
                if (i == 4)
                {
                    cs.Cancel();
                }
                return Task.CompletedTask;
            }, cs.Token).Wait();
            visitedItems.SequenceEqual(enumerable).ShouldBeFalse();
            visitedItems.ShouldContain(3);
            visitedItems.ShouldContain(4);
            visitedItems.ShouldNotContain(5);
        }

        [Fact]
        public void AnyAsync()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            enumerable.AnyAsync(i => Task.FromResult(i == 4)).Result.ShouldBeTrue();
            enumerable.AnyAsync(i => Task.FromResult(i == 6)).Result.ShouldBeFalse();
            enumerable.AnyAsync((i, c) => Task.FromResult(i == 4)).Result.ShouldBeTrue();
            enumerable.AnyAsync((i, c) => Task.FromResult(i == 6)).Result.ShouldBeFalse();
            var cs = new CancellationTokenSource();
            Should.Throw<OperationCanceledException>(() => enumerable.AnyAsync(i =>
            {
                if (i == 3)
                {
                    cs.Cancel();
                }
                return Task.FromResult(i == 4);
            }, cs.Token));
            cs = new CancellationTokenSource();
            Should.Throw<OperationCanceledException>(() => enumerable.AnyAsync((i, c) =>
            {
                if (i == 3)
                {
                    cs.Cancel();
                }
                return Task.FromResult(i == 4);
            }, cs.Token));

        }

        [Fact]
        public void AllAsync()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 5 };
            enumerable.AllAsync(i => Task.FromResult(i < 6)).Result.ShouldBeTrue();
            enumerable.AllAsync(i => Task.FromResult(i == 4)).Result.ShouldBeFalse();
            enumerable.AllAsync((i, c) => Task.FromResult(i < 6)).Result.ShouldBeTrue();
            enumerable.AllAsync((i, c) => Task.FromResult(i == 4)).Result.ShouldBeFalse();
            var cs = new CancellationTokenSource();
            Should.Throw<OperationCanceledException>(() => enumerable.AllAsync(i =>
            {
                if (i == 3)
                {
                    cs.Cancel();
                }
                return Task.FromResult(i < 6);
            }, cs.Token));
            cs = new CancellationTokenSource();
            Should.Throw<OperationCanceledException>(() => enumerable.AllAsync((i, c) =>
            {
                if (i == 3)
                {
                    cs.Cancel();
                }
                return Task.FromResult(i < 6);
            }, cs.Token));

        }
    }
}
