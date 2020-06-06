using Microsoft.EntityFrameworkCore;
using Scorpio.Data;
using Scorpio.Entities;
using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Scorpio.Threading;

namespace Scorpio.Repositories.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EfCoreRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IEfCoreRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {


        private readonly IDbContextProvider<TDbContext> _contextProvider;

        /// <summary>
        /// 
        /// </summary>
        protected virtual TDbContext DbContext => _contextProvider.GetDbContext();

        /// <summary>
        /// 
        /// </summary>
        internal protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        internal protected IQueryable<TEntity> Queryable => DbSet;

        DbContext IEfCoreRepository<TEntity>.DbContext => DbContext;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="contextProvider"></param>
        /// <param name="cancellationTokenProvider"></param>
        public EfCoreRepository(
            IServiceProvider serviceProvider,
            IDbContextProvider<TDbContext> contextProvider,
            ICancellationTokenProvider cancellationTokenProvider)
            : base(serviceProvider, cancellationTokenProvider)
        {
            _contextProvider = contextProvider;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity, bool autoSave = true)
        {
            var result = DbSet.Add(entity).Entity;
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            var result = await DbSet.AddAsync(entity, cancellationToken);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return result.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity, bool autoSave = true)
        {
            DbContext.Attach(entity);
            var result = DbContext.Update(entity).Entity;
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);
            var result = DbContext.Update(entity).Entity;
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        public override void Delete(TEntity entity, bool autoSave = true)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="autoSave"></param>
        public override void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = true)
        {
            var query = GetQueryable().IgnoreQueryFilters().Where(predicate);
            if (typeof(TEntity).IsAssignableTo<ISoftDelete>())
            {
                Expression<Func<SoftDelete, SoftDelete>> updator = d => new SoftDelete { IsDeleted = true };
                query.Update(updator.Translate<SoftDelete>().To<TEntity>());
                return;
            }
            query.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable().IgnoreQueryFilters().Where(predicate);
            if (typeof(TEntity).IsAssignableTo<ISoftDelete>())
            {
                Expression<Func<SoftDelete, SoftDelete>> updator = d => new SoftDelete { IsDeleted = true };
                await query.UpdateAsync(updator.Translate<SoftDelete>().To<TEntity>(), cancellationToken);
                return;
            }
            await query.DeleteAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <param name="autoSave"></param>
        public override void Update(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TEntity>> updateExpression, 
            bool autoSave = true)
        {
            GetQueryable().IgnoreQueryFilters().Where(predicate).Update(updateExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task UpdateAsync(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TEntity>> updateExpression,
            bool autoSave = true,
            CancellationToken cancellationToken = default)
        {
            await GetQueryable().IgnoreQueryFilters().Where(predicate).UpdateAsync(updateExpression, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveChanges()
        {
            if (DbContext?.ChangeTracker?.HasChanges() ?? false)
            {
                DbContext?.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (DbContext?.ChangeTracker?.HasChanges() ?? false)
            {
                await DbContext?.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<TEntity> GetQueryable()
        {
            return DbSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override long GetCount()
        {
            return GetQueryable().LongCount();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.LongCountAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public override IEnumerable<TEntity> GetList(bool includeDetails = false)
        {
            return includeDetails ? WithDetails().ToList() : DbSet.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().ToListAsync(cancellationToken)
                : await DbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
            CancellationToken cancellationToken = default)
            where TProperty : class
        {
            await DbContext.Entry(entity).Collection(propertyExpression).LoadAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken = default)
            where TProperty : class
        {
            await DbContext.Entry(entity).Reference(propertyExpression).LoadAsync(GetCancellationToken(cancellationToken));
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EfCoreRepository<TDbContext, TEntity, TKey> : 
        EfCoreRepository<TDbContext, TEntity>,
        IEfCoreRepository<TEntity, TKey>,
        ISupportsExplicitLoading<TEntity, TKey>
        
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="contextProvider"></param>
        /// <param name="cancellationTokenProvider"></param>
        public EfCoreRepository(
            IServiceProvider serviceProvider,
            IDbContextProvider<TDbContext> contextProvider,
            ICancellationTokenProvider cancellationTokenProvider) 
            : base(serviceProvider, contextProvider, cancellationTokenProvider)
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
        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

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
        /// <returns></returns>
        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return includeDetails
                ? WithDetails().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id))
                : DbSet.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().FirstOrDefaultAsync(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id), GetCancellationToken(cancellationToken))
                : await DbSet.FindAsync(new object[] { id }, GetCancellationToken(cancellationToken));
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
        public virtual async Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails: false, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }
            await DeleteAsync(entity, autoSave, cancellationToken);
        }
    }

}
