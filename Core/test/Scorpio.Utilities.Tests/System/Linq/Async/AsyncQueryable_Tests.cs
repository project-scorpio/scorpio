using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


using Moq;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncQueryable_Tests
    {


        private IQueryable<TSource> GetQueryable<TSource, TResult>(TResult result = default)
        {
            var mock = new Mock<ITestQueryable<TSource>>();
            var providerMock = new Mock<IAsyncQueryProvider>();
            var enumeratorMock = new Mock<IAsyncEnumerator<TSource>>();
            var index = 0;
            enumeratorMock.SetupGet(e => e.Current).Returns(default(TSource));
            enumeratorMock.Setup(e => e.MoveNextAsync()).Returns(() => new ValueTask<bool>(index++ < 1));
            providerMock.Setup(e => e.ExecuteAsync<Task<TResult>>(It.IsAny<Expression>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(result));
            mock.SetupGet(e => e.Provider).Returns(providerMock.Object);
            mock.SetupGet(e => e.Expression).Returns(new List<TSource> { }.AsQueryable().Expression);
            mock.Setup(e => e.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(enumeratorMock.Object);
            return mock.Object;
        }

        [Fact]
        public void AnyAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.AnyAsync());
            Should.Throw<InvalidOperationException>(async () => await source.AnyAsync(a => default));
            GetQueryable<int, bool>(true).AnyAsync().Result.ShouldBeTrue();
            GetQueryable<int, bool>(true).AnyAsync(a => default).Result.ShouldBeTrue();
        }

        [Fact]
        public void AllAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.AllAsync(a => default));
            GetQueryable<int, bool>(true).AllAsync(a => default).Result.ShouldBeTrue();
        }

        [Fact]
        public void AverageAsync()
        {
            GetQueryable<int, double>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<int?, double?>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<int, double>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<int?, double?>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<int>, double>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<int?>, double?>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<long, double>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<long?, double?>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<long, double>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<long?, double?>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<long>, double>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<long?>, double?>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<decimal, double>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<decimal?, double?>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<decimal, double>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<decimal?, double?>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<decimal>, double>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<decimal?>, double?>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<double, double>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<double?, double?>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<double, double>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<double?, double?>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<double>, double>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<double?>, double?>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<float, double>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<float?, double?>().AverageAsync().Result.ShouldBe(default);
            GetQueryable<float, double>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<float?, double?>().AverageAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<float>, double>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<float?>, double?>().AverageAsync(v => v.Value).Result.ShouldBe(default);
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.AsQueryable().AverageAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new int?[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new int?[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.AsQueryable().AverageAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new long?[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new long?[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.AsQueryable().AverageAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new decimal?[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new decimal?[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.AsQueryable().AverageAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new double?[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new double?[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.AsQueryable().AverageAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new float?[] { default }.Select(i => new { Value = i }).AsQueryable().AverageAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.AsQueryable().AverageAsync());
            Should.Throw<InvalidOperationException>(async () => await new float?[] { default }.AsQueryable().AverageAsync());
        }

        [Fact]
        public void SumAsync()
        {
            GetQueryable<int, int>().SumAsync().Result.ShouldBe(default);
            GetQueryable<int?, int?>().SumAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<int?, int?>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<int>, int>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<int?>, int?>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<long, long>().SumAsync().Result.ShouldBe(default);
            GetQueryable<long?, long?>().SumAsync().Result.ShouldBe(default);
            GetQueryable<long, long>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<long?, long?>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<long>, long>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<long?>, long?>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<decimal, decimal>().SumAsync().Result.ShouldBe(default);
            GetQueryable<decimal?, decimal?>().SumAsync().Result.ShouldBe(default);
            GetQueryable<decimal, decimal>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<decimal?, decimal?>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<decimal>, decimal>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<decimal?>, decimal?>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<double, double>().SumAsync().Result.ShouldBe(default);
            GetQueryable<double?, double?>().SumAsync().Result.ShouldBe(default);
            GetQueryable<double, double>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<double?, double?>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<double>, double>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<double?>, double?>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<float, float>().SumAsync().Result.ShouldBe(default);
            GetQueryable<float?, float?>().SumAsync().Result.ShouldBe(default);
            GetQueryable<float, float>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<float?, float?>().SumAsync(a => default).Result.ShouldBe(default);
            GetQueryable<TestObj<float>, float>().SumAsync(v => v.Value).Result.ShouldBe(default);
            GetQueryable<TestObj<float?>, float?>().SumAsync(v => v.Value).Result.ShouldBe(default);
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.AsQueryable().SumAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new int?[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new int[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new int?[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.AsQueryable().SumAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new long?[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new long[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new long?[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.AsQueryable().SumAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new decimal?[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new decimal[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new decimal?[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.AsQueryable().SumAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new double?[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new double[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new double?[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.AsQueryable().SumAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new float?[] { default }.Select(i => new { Value = i }).AsQueryable().SumAsync(a => a.Value));
            Should.Throw<InvalidOperationException>(async () => await new float[] { default }.AsQueryable().SumAsync());
            Should.Throw<InvalidOperationException>(async () => await new float?[] { default }.AsQueryable().SumAsync());
        }

        [Fact]
        public void FirstAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.FirstAsync());
            Should.Throw<InvalidOperationException>(async () => await source.FirstAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await source.FirstOrDefaultAsync());
            Should.Throw<InvalidOperationException>(async () => await source.FirstOrDefaultAsync(a => default));
            GetQueryable<int, int>().FirstAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().FirstAsync(a => default).Result.ShouldBe(default);
            GetQueryable<int, int>().FirstOrDefaultAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().FirstOrDefaultAsync(a => default).Result.ShouldBe(default);
        }

        [Fact]
        public void ForEachAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ForEachAsync(i => { }));
            var list = new List<int>();
            GetQueryable<int, int>().ForEachAsync(a => list.Add(a)).Wait();
            list.ShouldHaveSingleItem().ShouldBe(default);
        }

        [Fact]
        public void LastAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.LastAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LastAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await source.LastOrDefaultAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LastOrDefaultAsync(a => default));
            GetQueryable<int, int>().LastAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().LastAsync(a => default).Result.ShouldBe(default);
            GetQueryable<int, int>().LastOrDefaultAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().LastOrDefaultAsync(a => default).Result.ShouldBe(default);
        }

        [Fact]
        public void LongCountAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.LongCountAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LongCountAsync(a => default));
            GetQueryable<int, long>().LongCountAsync().Result.ShouldBe(default);
            GetQueryable<int, long>().LongCountAsync(a => default).Result.ShouldBe(default);
        }

        [Fact]
        public void ContainsAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ContainsAsync(default));
            GetQueryable<int, bool>().ContainsAsync(default).Result.ShouldBe(default);
        }

        [Fact]
        public void CountAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.CountAsync());
            Should.Throw<InvalidOperationException>(async () => await source.CountAsync(a => default));
            GetQueryable<int, int>().CountAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().CountAsync(a => default).Result.ShouldBe(default);
        }


        [Fact]
        public void MaxAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.MaxAsync());
            Should.Throw<InvalidOperationException>(async () => await source.Select(i => new { Value = i }).MaxAsync(a => a.Value));
            GetQueryable<int, int>().MaxAsync().Result.ShouldBe(default);
            GetQueryable<TestObj<int>, int>().MaxAsync(a => a.Value).Result.ShouldBe(default);
        }

        [Fact]
        public void MinAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.MinAsync());
            Should.Throw<InvalidOperationException>(async () => await source.Select(i => new { Value = i }).MinAsync(a => a.Value));
            GetQueryable<int, int>().MinAsync().Result.ShouldBe(default);
            GetQueryable<TestObj<int>, int>().MinAsync(a => a.Value).Result.ShouldBe(default);
        }

        [Fact]
        public void SingleAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.SingleAsync());
            Should.Throw<InvalidOperationException>(async () => await source.SingleAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await source.SingleOrDefaultAsync());
            Should.Throw<InvalidOperationException>(async () => await source.SingleOrDefaultAsync(a => default));
            GetQueryable<int, int>().SingleAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().SingleAsync(a => default).Result.ShouldBe(default);
            GetQueryable<int, int>().SingleOrDefaultAsync().Result.ShouldBe(default);
            GetQueryable<int, int>().SingleOrDefaultAsync(a => default).Result.ShouldBe(default);
        }

        [Fact]
        public void ToArrayAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ToArrayAsync());
        }

        [Fact]
        public void ToDictionaryAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i));
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i, i => i));
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i, EqualityComparer<int>.Default));
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i, i => i, EqualityComparer<int>.Default));
            GetQueryable<int, int>().ToDictionaryAsync(i => i).Result.ShouldHaveSingleItem().Key.ShouldBe(default);
            GetQueryable<int, int>().ToDictionaryAsync(i => i, i => i).Result.ShouldHaveSingleItem().Key.ShouldBe(default);
            GetQueryable<int, int>().ToDictionaryAsync(i => i, EqualityComparer<int>.Default).Result.ShouldHaveSingleItem().Key.ShouldBe(default);
            GetQueryable<int, int>().ToDictionaryAsync(i => i, i => i, EqualityComparer<int>.Default).Result.ShouldHaveSingleItem().Key.ShouldBe(default);
        }

        [Fact]
        public void ToListAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ToListAsync());
            GetQueryable<int, int>().ToListAsync().Result.ShouldHaveSingleItem().ShouldBe(default);
        }

        public class TestObj<T>
        {
            public virtual T Value { get; set; }
        }

        public interface ITestQueryable<out T> : IQueryable<T>, IAsyncEnumerable<T>
        {

        }
    }
}
