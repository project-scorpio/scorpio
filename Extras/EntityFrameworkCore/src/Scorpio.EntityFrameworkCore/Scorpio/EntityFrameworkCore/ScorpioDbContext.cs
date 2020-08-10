using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Scorpio.Data;
using Scorpio.Entities;
using Scorpio.Threading;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class ScorpioDbContext<TDbContext> : ScorpioDbContext
        where TDbContext : ScorpioDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextOptions"></param>
        /// <param name="filterOptions"></param>
        /// <param name="serviceProvider"></param>
        protected ScorpioDbContext(IServiceProvider serviceProvider, DbContextOptions<TDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions)
            : base(serviceProvider, contextOptions, filterOptions)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScorpioDbContext : DbContext, IFilterContext
    {
        private readonly DataFilterOptions _filterOptions;


        /// <summary>
        /// 
        /// </summary>
        public IOnSaveChangeHandlersFactory OnSaveChangeHandlersFactory { get; }
        /// <summary>
        /// 
        /// </summary>
        public IDataFilter DataFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScorpioDbContextOptions ScorpioDbContextOptions { get; }

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        private readonly MethodInfo _configureEntityPropertiesMethodInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextOptions"></param>
        /// <param name="filterOptions"></param>
        /// <param name="serviceProvider"></param>
        protected ScorpioDbContext(IServiceProvider serviceProvider, DbContextOptions contextOptions, IOptions<DataFilterOptions> filterOptions)
            : base(contextOptions)
        {
            _configureEntityPropertiesMethodInfo = ((Action<ModelBuilder, IMutableEntityType>)ConfigureEntityProperties<object>).Method.GetGenericMethodDefinition();
            _filterOptions = filterOptions.Value;
            ServiceProvider = serviceProvider;
            OnSaveChangeHandlersFactory = ServiceProvider.GetService<IOnSaveChangeHandlersFactory>();
            Logger = ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(this.GetType());
            DataFilter = ServiceProvider.GetService<IDataFilter>();
            ScorpioDbContextOptions = ServiceProvider.GetRequiredService<IOptions<ScorpioDbContextOptions>>().Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                _configureEntityPropertiesMethodInfo
                   .MakeGenericMethod(entityType.ClrType)
                   .Invoke(this, new object[] { modelBuilder, entityType });
            }
            base.OnModelCreating(modelBuilder);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entityChangeList = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            var saveChangeHandlers = OnSaveChangeHandlersFactory.CreateHandlers();
            if (entityChangeList.Count > 0)
            {
                saveChangeHandlers.ForEach(handler => AsyncHelper.RunSync(() => handler.PreSaveChangeAsync(entityChangeList)));
            }
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            if (entityChangeList.Count > 0)
            {
                saveChangeHandlers.ForEach(handler => AsyncHelper.RunSync(() => handler.PostSaveChangeAsync(entityChangeList)));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityChangeList = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            var saveChangeHandlers = OnSaveChangeHandlersFactory.CreateHandlers();
            if (entityChangeList.Count > 0)
            {
                await saveChangeHandlers.ForEachAsync(async handler => await handler.PreSaveChangeAsync(entityChangeList));
            }
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (entityChangeList.Count > 0)
            {
                await saveChangeHandlers.ForEachAsync(async handler => await handler.PostSaveChangeAsync(entityChangeList));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        protected void ConfigureEntityProperties<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            ConfigureCreatingContributor<TEntity>(modelBuilder, entityType);
            ConfigureGlobalFilters<TEntity>(modelBuilder, entityType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        protected void ConfigureCreatingContributor<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            var context = new ModelCreatingContributionContext(modelBuilder, entityType);
            var modelCreatingContributors = ScorpioDbContextOptions.GetModelCreatingContributors(GetType());
            if (modelCreatingContributors != null)
            {
                foreach (var item in modelCreatingContributors)
                {
                    item.Contributor<TEntity>(context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            return _filterOptions.Descriptors.Keys.Any(t => t.IsAssignableFrom(typeof(TEntity)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;
            _filterOptions.Descriptors.ForEach(item =>
            {
                if (item.Key.IsAssignableFrom(typeof(TEntity)))
                {
                    var filterexpression = item.Value.BuildFilterExpression<TEntity>(DataFilter, this);
                    filterexpression = filterexpression.OrElse(filterexpression.Equal(expr2 => DataFilter.IsEnabled(item.Key)));
                    expression = expression == null ? filterexpression : expression.AndAlso(filterexpression);
                }
            });
            return expression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParameter<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public Expression<Func<TEntity, TProperty>> GetPropertyExpression<TEntity, TProperty>(string propertyName)
        {
            return e => EF.Property<TProperty>(e, propertyName);
        }
    }
}
