using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// if unit of work is used in an async scope.
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }


        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object.
        /// </summary>
        public UnitOfWorkOptions()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UnitOfWorkOptions Clone()
        {
            return new UnitOfWorkOptions {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                AsyncFlowOption = AsyncFlowOption,
                Scope = Scope,
                Timeout = Timeout
            };
        }

    }
}
