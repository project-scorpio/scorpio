using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;
using Scorpio.EntityFrameworkCore;
using Scorpio.EntityFrameworkCore.DependencyInjection;

namespace Scorpio.Uow
{
    internal class UnitOfWorkEfTransactionStrategy : IEfTransactionStrategy, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _disposedValue;

        public UnitOfWorkOptions Options { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, TransactionDescriptor> ActiveTransactions { get; } = new Dictionary<string, TransactionDescriptor>();



        public UnitOfWorkEfTransactionStrategy(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public TDbContext CreateDbContext<TDbContext>(string connectionString) where TDbContext : ScorpioDbContext<TDbContext>
        {
            var key = $"Transaction_{connectionString}";
            var descriptor = ActiveTransactions.GetOrDefault(key);
            TDbContext dbContext;
            if (descriptor == null)
            {
                dbContext = _serviceProvider.GetRequiredService<TDbContext>();
                var transaction = Options.IsolationLevel.HasValue
                    ? dbContext.Database.BeginTransaction(Options.IsolationLevel.Value.ToSystemDataIsolationLevel())
                    : dbContext.Database.BeginTransaction();
                descriptor = ActiveTransactions[key] = new TransactionDescriptor(transaction);
            }
            else
            {
                var connection = descriptor.Transaction.GetDbTransaction().Connection;
                DbContextCreationContext.Current.ExistingConnection = connection;
                dbContext = _serviceProvider.GetRequiredService<TDbContext>();
                if (dbContext.HasRelationalTransactionManager())
                {
                    dbContext.Database.UseTransaction(descriptor.Transaction.GetDbTransaction());
                }
                else
                {
                    dbContext.Database.BeginTransaction();
                }
            }
            descriptor.AddContext(dbContext);
            return dbContext;
        }

        public void Commit() => ActiveTransactions.Values.ForEach(tran => tran.Commit());




        public void InitOptions(UnitOfWorkOptions options) => Options = options;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    ActiveTransactions.Values.ForEach(tran => tran.Dispose());
                    ActiveTransactions.Clear();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
