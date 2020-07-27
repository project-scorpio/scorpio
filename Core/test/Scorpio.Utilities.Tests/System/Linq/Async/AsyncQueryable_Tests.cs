using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace System.Linq.Async
{
    public class AsyncQueryable_Tests
    {
        [Fact]
        public void AnyAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.AnyAsync());
            Should.Throw<InvalidOperationException>(async () => await source.AnyAsync(a => default));
        }

        [Fact]
        public void AllAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.AllAsync(a => default));
        }

        [Fact]
        public void AverageAsync()
        {
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
        }

        [Fact]
        public void ForEachAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ForEachAsync(i=> { }));
        }

        [Fact]
        public void LastAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.LastAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LastAsync(a=>default));
            Should.Throw<InvalidOperationException>(async () => await source.LastOrDefaultAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LastOrDefaultAsync(a=>default));
        }

        [Fact]
        public void LongCountAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.LongCountAsync());
            Should.Throw<InvalidOperationException>(async () => await source.LongCountAsync(a => default));
        }

        [Fact]
        public void ContainsAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ContainsAsync(default));
        }

        [Fact]
        public void CountAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.CountAsync());
            Should.Throw<InvalidOperationException>(async () => await source.CountAsync(a => default));
        }


        [Fact]
        public void MaxAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.MaxAsync());
            Should.Throw<InvalidOperationException>(async () => await source.Select(i => new { Value = i }).MaxAsync(a => a.Value));
        }

        [Fact]
        public void MinAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.MinAsync());
            Should.Throw<InvalidOperationException>(async () => await source.Select(i => new { Value = i }).MinAsync(a => a.Value));
        }

        [Fact]
        public void SingleAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.SingleAsync());
            Should.Throw<InvalidOperationException>(async () => await source.SingleAsync(a => default));
            Should.Throw<InvalidOperationException>(async () => await source.SingleOrDefaultAsync());
            Should.Throw<InvalidOperationException>(async () => await source.SingleOrDefaultAsync(a => default));
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
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i,  EqualityComparer<int>.Default));
            Should.Throw<InvalidOperationException>(async () => await source.ToDictionaryAsync(i => i, i => i, EqualityComparer<int>.Default));
        }

        [Fact]
        public void ToListAsync()
        {
            var source = new int[] { default }.AsQueryable();
            Should.Throw<InvalidOperationException>(async () => await source.ToListAsync());
        }

    }
}
