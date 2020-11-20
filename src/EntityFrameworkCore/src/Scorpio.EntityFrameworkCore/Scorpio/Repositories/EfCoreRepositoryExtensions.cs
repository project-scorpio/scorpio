using System;

using Microsoft.EntityFrameworkCore;

using Scorpio.Entities;
using Scorpio.Repositories.EntityFrameworkCore;

namespace Scorpio.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public static class EfCoreRepositoryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static DbContext GetDbContext<TEntity, TKey>(this IBasicRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey> => repository.ToEfCoreRepository().DbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static IEfCoreRepository<TEntity, TKey> ToEfCoreRepository<TEntity, TKey>(this IBasicRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
        {
            if (repository is not IEfCoreRepository<TEntity, TKey> efCoreRepository)
            {
                throw new ArgumentException("Given repository does not implement " + typeof(IEfCoreRepository<TEntity, TKey>).AssemblyQualifiedName, nameof(repository));
            }

            return efCoreRepository;
        }
    }
}
