using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Scorpio.EntityFrameworkCore;

namespace Scorpio.Uow
{
    internal class TransactionDescriptor : IDisposable
    {
        private readonly HashSet<DbContext> _dbContexts = new HashSet<DbContext>();

        public IDbContextTransaction Transaction { get; }

        public IEnumerable<DbContext> DbContexts => _dbContexts.ToImmutableList();

        public TransactionDescriptor(IDbContextTransaction transaction)
        {
            Transaction = transaction;
        }

        public void AddContext(DbContext context)
        {
            _dbContexts.Add(context);
        }

        public void Commit()
        {
            Transaction.Commit();
            _dbContexts.ForEach(context =>
            {
                if (!context.HasRelationalTransactionManager())
                {
                    context.Database.CommitTransaction();
                }
            });
        }

        #region IDisposable Support
        private bool _disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Transaction.Dispose();
                    _dbContexts.ForEach(context => context.Dispose());
                }


                _disposedValue = true;
            }
        }


        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
