using Scorpio.DependencyInjection;
using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Scorpio.EntityFrameworkCore.DependencyInjection;

namespace Scorpio.Uow
{
    internal class UnitOfWorkEfTransactionStrategy : IEfTransactionStrategy,ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkOptions Options { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, TransactionDescriptor> ActiveTransactions { get; } = new Dictionary<string, TransactionDescriptor>();



        public UnitOfWorkEfTransactionStrategy(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDbContext CreateDbContext<TDbContext>(string connectionString) where TDbContext : ScorpioDbContext<TDbContext>
        {
            TDbContext dbContext = null;
            var key = $"Transaction_{connectionString}";
            var descriptor = ActiveTransactions.GetOrDefault(key);
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

        public void Commit()
        {
            ActiveTransactions.Values.ForEach(tran => tran.Commit());
        }


        public void Dispose()
        {
            ActiveTransactions.Values.ForEach(tran => tran.Dispose());
            ActiveTransactions.Clear();
        }

        public void InitOptions(UnitOfWorkOptions options)
        {
            Options = options;
        }


    }
}
