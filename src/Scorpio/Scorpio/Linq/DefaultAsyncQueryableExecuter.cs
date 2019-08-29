using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Scorpio.Linq
{
    internal class DefaultAsyncQueryableExecuter : IAsyncQueryableExecuter, ISingletonDependency
    {
        public static DefaultAsyncQueryableExecuter Instance { get; } = new DefaultAsyncQueryableExecuter();

        public Task<int> CountAsync<T>(IQueryable<T> queryable,CancellationToken cancellationToken=default)
        {
            return Task.FromResult(queryable.Count());
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.ToList());
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.FirstOrDefault());
        }

        public IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(IQueryable<TSource> sources)
        {
            return sources.ToAsyncEnumerable();
        }
    }
}
