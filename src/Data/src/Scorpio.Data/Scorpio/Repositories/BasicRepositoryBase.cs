using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Scorpio.DependencyInjection;
using Scorpio.Entities;
using Scorpio.Threading;

namespace Scorpio.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BasicRepositoryBase<TEntity> :
        IBasicRepository<TEntity>,
        ITransientDependency
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICancellationTokenProvider CancellationTokenProvider { get; set; }

        private protected Task Invoke(Action action, CancellationToken cancellationToken) => Task.Run(action, CancellationTokenProvider.FallbackToProvider(cancellationToken));
        private protected Task<TResult> Invoke<TResult>(Func<TResult> action, CancellationToken cancellationToken) => Task.Run(action, GetCancellationToken(cancellationToken));

        /// <summary>
        /// 
        /// </summary>
        protected BasicRepositoryBase() => CancellationTokenProvider = NoneCancellationTokenProvider.Instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public abstract TEntity Insert(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Insert(entity, autoSave), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public abstract TEntity Update(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Update(entity), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        public abstract void Delete(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Delete(entity), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefferedValue"></param>
        /// <returns></returns>
        protected virtual CancellationToken GetCancellationToken(CancellationToken prefferedValue = default) => CancellationTokenProvider.FallbackToProvider(prefferedValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public abstract IEnumerable<TEntity> GetList(bool includeDetails = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default) => Invoke(() => GetList(includeDetails), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract long GetCount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<long> GetCountAsync(CancellationToken cancellationToken = default) => Invoke(() => GetCount(), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        public virtual void SaveChanges()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SaveChangesAsync(CancellationToken cancellationToken = default) => Invoke(() => SaveChanges(), cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BasicRepositoryBase<TEntity, TKey> : BasicRepositoryBase<TEntity>, IBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        protected BasicRepositoryBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey id, bool includeDetails = true)
        {
            var entity = Find(id, includeDetails);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default) => Invoke(() => Get(id, includeDetails), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public abstract TEntity Find(TKey id, bool includeDetails = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default) => Invoke(() => Find(id, includeDetails), cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        public virtual void Delete(TKey id, bool autoSave = true)
        {
            var entity = Find(id);
            if (entity == null)
            {
                return;
            }

            Delete(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Delete(id), cancellationToken);
    }
}
