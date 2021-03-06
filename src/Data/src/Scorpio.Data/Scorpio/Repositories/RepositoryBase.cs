﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Scorpio.Entities;

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
        protected RepositoryBase()
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
        public virtual IQueryable<TEntity> WithDetails() => WithDetails(new Expression<Func<TEntity, object>>[0]);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors) => GetQueryable();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntity> GetEnumerator() => GetQueryable().GetEnumerator();

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
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Delete(predicate, autoSave), cancellationToken);

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
                Value = Expression.Lambda(a.Expression).Compile().DynamicInvoke()
            });
            var type = typeof(TEntity);
            members.ForEach(m => type.GetProperty(m.Name).SetValue(entity, m.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Update(predicate, updateExpression), cancellationToken);
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
        protected RepositoryBase()
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default) => Invoke(() => Find(id, includeDetails), cancellationToken);

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
        public virtual Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default) => Invoke(() => Delete(id, autoSave), cancellationToken);
    }
}
