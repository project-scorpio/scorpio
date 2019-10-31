using Scorpio.Entities;
using Scorpio.DynamicProxy;
using Scorpio.Threading;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task EnsureCollectionLoadedAsync<TEntity, TKey, TProperty>(
            this IBasicRepository<TEntity, TKey> repository,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
            CancellationToken cancellationToken = default
        )
            where TEntity : class, IEntity<TKey>
            where TProperty : class
        {

            if (repository.UnProxy() is ISupportsExplicitLoading<TEntity, TKey> repo)
            {
                await repo.EnsureCollectionLoadedAsync(entity, propertyExpression, cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        public static void EnsureCollectionLoaded<TEntity, TKey, TProperty>(
            this IBasicRepository<TEntity, TKey> repository,
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression
        )
            where TEntity : class, IEntity<TKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsureCollectionLoadedAsync(entity, propertyExpression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task EnsurePropertyLoadedAsync<TEntity, TKey, TProperty>(
            this IBasicRepository<TEntity, TKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken = default
        )
            where TEntity : class, IEntity<TKey>
            where TProperty : class
        {
            if (repository.UnProxy() is ISupportsExplicitLoading<TEntity, TKey> repo)
            {
                await repo.EnsurePropertyLoadedAsync(entity, propertyExpression, cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        public static void EnsurePropertyLoaded<TEntity, TKey, TProperty>(
            this IBasicRepository<TEntity, TKey> repository,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression
        )
            where TEntity : class, IEntity<TKey>
            where TProperty : class
        {
            AsyncHelper.RunSync(() => repository.EnsurePropertyLoadedAsync(entity, propertyExpression));
        }
    }
}
