using Scorpio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Scorpio.Threading;

namespace Scorpio.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : BasicRepositoryBase<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="cancellationTokenProvider"></param>
        protected RepositoryBase(IServiceProvider serviceProvider, ICancellationTokenProvider cancellationTokenProvider) : base(serviceProvider, cancellationTokenProvider)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Type ElementType => GetQueryable().ElementType;

        /// <summary>
        /// 
        /// </summary>
        public virtual Expression Expression => GetQueryable().Expression;

        /// <summary>
        /// 
        /// </summary>
        public virtual IQueryProvider Provider => GetQueryable().Provider;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> WithDetails()
        {
            return GetQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetQueryable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetQueryable().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="autoSave"></param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = true)
        {
            foreach (var entity in GetQueryable().Where(predicate).ToList())
            {
                Delete(entity, autoSave);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            Delete(predicate, autoSave);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <param name="autoSave"></param>
        public virtual void Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression, bool autoSave = true)
        {
            foreach (var entity in GetQueryable().Where(predicate).ToList())
            {
                SetValue(entity, updateExpression);
                Update(entity, autoSave);
            }
        }

        private void SetValue(TEntity entity, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            var body = updateExpression.Body as MemberInitExpression;
            var members = body.Bindings.Cast<MemberAssignment>().Select(a => new
            {
                a.Member.Name,
                Value=Expression.Lambda(a.Expression).Compile().DynamicInvoke()
            });
            var type = typeof(TEntity);
            members.ForEach(m =>
            {
                type.GetProperty(m.Name).SetValue(entity, m.Value);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            Update(predicate, updateExpression);
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class RepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="cancellationTokenProvider"></param>
        protected RepositoryBase(IServiceProvider serviceProvider, ICancellationTokenProvider cancellationTokenProvider) : base(serviceProvider, cancellationTokenProvider)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return includeDetails
                ? WithDetails().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id))
                : GetQueryable().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id));
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
        public virtual Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Get(id, includeDetails));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Find(id, includeDetails));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        public virtual void Delete(TKey id, bool autoSave = true)
        {
            var entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            Delete(id, autoSave);
            return Task.CompletedTask;
        }
    }
}
