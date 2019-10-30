using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Scorpio.Linq
{
    internal class EntityFrameworkCoreAsyncQueryableExecuter : IAsyncQueryableExecuter, ISingletonDependency
    {
        public static EntityFrameworkCoreAsyncQueryableExecuter Instance { get; } = new EntityFrameworkCoreAsyncQueryableExecuter();

        public Task<int> CountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.CountAsync(cancellationToken);
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.ToListAsync(cancellationToken);
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.FirstOrDefaultAsync(cancellationToken);
        }

        public IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(IQueryable<TSource> sources)
        {
            return sources.AsAsyncEnumerable();
        }
    }
}
