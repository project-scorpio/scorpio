using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitOfWorkAttribute : AbstractInterceptorAttribute
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
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var interceptor = context.ServiceProvider.GetService<UnitOfWorkInterceptor>();
            interceptor.SetOptions(this);
            return interceptor.Invoke(context, next);
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

            if (options.IsTransactional == null)
            {
                options.IsTransactional = IsTransactional;
            }

            if (options.Scope == null)
            {
                options.Scope = Scope;
            }
            return options;
        }
    }
}
