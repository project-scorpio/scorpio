using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scorpio.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using System.Threading;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase
    {
        private readonly IEfTransactionStrategy _transactionStrategy;

        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, ScorpioDbContext> ActiveDbContexts { get; } = new Dictionary<string, ScorpioDbContext>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="transactionStrategy"></param>
        /// <param name="options"></param>
        public EfUnitOfWork(IServiceProvider serviceProvider, IEfTransactionStrategy transactionStrategy, IOptions<UnitOfWorkDefaultOptions> options) : base(serviceProvider, options)
        {
            _transactionStrategy = transactionStrategy;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveChanges()
        {
            foreach (var item in GetAllActiveDbContexts())
            {
                item.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            foreach (var item in GetAllActiveDbContexts())
            {
                await item.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.InitOptions(Options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CompleteUow()
        {
            SaveChanges();
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task CompleteUowAsync(CancellationToken cancellationToken=default)
        {
            await SaveChangesAsync(cancellationToken);
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DisposeUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Dispose();
            }
            else
            {
                GetAllActiveDbContexts().ForEach(context => context.Dispose());
            }
            ActiveDbContexts.Clear();
        }

        /// <summary>
        /// 获取全部活动的DBContext
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ScorpioDbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>(string connectionString)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            var connectionKey = $"DbContext_{typeof(TDbContext).FullName}_{connectionString}";
            return ActiveDbContexts.GetOrAdd(connectionKey, key => CreateDbContext<TDbContext>(connectionString)) as TDbContext;
        }

        private TDbContext CreateDbContext<TDbContext>(string connectionString)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            using (DbContextCreationContext.Use(new DbContextCreationContext(connectionString)))
            {
                var context = Options.IsTransactional ?? true ?
                    CreateDbContextWithTransactional<TDbContext>(connectionString) :
                    ServiceProvider.GetService<TDbContext>();
                if (Options.Timeout.HasValue &&
                    context.Database.IsRelational() &&
                    context.Database.GetCommandTimeout().HasValue)
                {
                    context.Database.SetCommandTimeout(Options.Timeout.Value);
                }
                return context;
            }
        }

        private TDbContext CreateDbContextWithTransactional<TDbContext>(string connectionString)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            return _transactionStrategy.CreateDbContext<TDbContext>(connectionString);
        }
    }
}
