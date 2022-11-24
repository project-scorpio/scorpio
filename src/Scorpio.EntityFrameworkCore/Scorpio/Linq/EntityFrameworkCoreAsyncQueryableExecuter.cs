using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Scorpio.DependencyInjection;
namespace Scorpio.Linq
{
    internal class EntityFrameworkCoreAsyncQueryableExecuter : IAsyncQueryableExecuter, ISingletonDependency
    {
        public static EntityFrameworkCoreAsyncQueryableExecuter Instance { get; } = new EntityFrameworkCoreAsyncQueryableExecuter();

        public Task<int> CountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.CountAsync(cancellationToken);
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default) => queryable.ToListAsync(cancellationToken);

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default) => queryable.FirstOrDefaultAsync(cancellationToken);

        public IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(IQueryable<TSource> sources) => sources.AsAsyncEnumerable();
    }
}
