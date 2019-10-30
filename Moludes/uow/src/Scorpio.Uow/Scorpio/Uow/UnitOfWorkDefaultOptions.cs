using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        public TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// </summary>
        public bool IsTransactional { get; set; }

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
        /// 
        /// </summary>
        public UnitOfWorkDefaultOptions()
        {
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }

        internal UnitOfWorkOptions Normalize(UnitOfWorkOptions options)
        {
            if (options.IsolationLevel == null)
            {
                options.IsolationLevel = IsolationLevel;
            }

            if (options.Timeout == null)
            {
                options.Timeout = Timeout;
            }

            if (options.IsTransactional==null)
            {
                options.IsTransactional = IsTransactional;
            }

            if (options.Scope==null)
            {
                options.Scope = Scope;
            }
            return options;
        }

    }
}
