using System.Reflection;
using System.Threading.Tasks;


using Microsoft.Extensions.Options;

using Scorpio.DynamicProxy;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UnitOfWorkDefaultOptions _defaultOptions;
        private UnitOfWorkAttribute _optionsAttribute;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="options"></param>
        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager, IOptions<UnitOfWorkDefaultOptions> options)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _defaultOptions = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public async Task InterceptAsync(IMethodInvocation invocation)
        {
            var options = CreateOptions();
            if (invocation.Method.AttributeExists<DisableUnitOfWorkAttribute>())
            {
                options.Scope = System.Transactions.TransactionScopeOption.Suppress;
            }
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                await invocation.ProceedAsync();
                if (invocation.IsAsync())
                {
                    await uow.CompleteAsync(); 
                }
                else
                {
                    uow.Complete();
                }
            }
        }

        internal void SetOptions(UnitOfWorkAttribute options) => _optionsAttribute = options;

        private UnitOfWorkOptions CreateOptions()
        {
            var options = new UnitOfWorkOptions();
            _defaultOptions.Normalize(options);
            _optionsAttribute?.Normalize(options);
            return options;
        }
    }
}
