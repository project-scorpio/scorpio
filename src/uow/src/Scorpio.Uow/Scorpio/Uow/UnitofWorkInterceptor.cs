using System.Reflection;
using System.Threading.Tasks;

using AspectCore.DynamicProxy;

using Microsoft.Extensions.Options;
namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkInterceptor : AbstractInterceptor
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
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var options = CreateOptions();
            if (context.ServiceMethod.AttributeExists<DisableUnitOfWorkAttribute>() || context.ImplementationMethod.AttributeExists<DisableUnitOfWorkAttribute>())
            {
                options.Scope = System.Transactions.TransactionScopeOption.Suppress;
            }
            using (var uow = _unitOfWorkManager.Begin(options))
            {
             await   context.Invoke(next);
                if (context.IsAsync())
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
