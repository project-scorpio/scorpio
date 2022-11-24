using System;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace Scorpio.Castle.DynamicProxy
{
    internal class CastleAsyncInterceptorAdapter: AsyncInterceptorBase
    {
        private readonly Scorpio.DynamicProxy.IInterceptor _abpInterceptor;

        public CastleAsyncInterceptorAdapter(Scorpio.DynamicProxy.IInterceptor abpInterceptor) => _abpInterceptor = abpInterceptor;

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            await _abpInterceptor.InterceptAsync(
                new CastleMethodInvocationAdapter(invocation, proceedInfo, proceed)
            );
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var adapter = new CastleMethodInvocationAdapter<TResult>(invocation, proceedInfo, proceed);

            await _abpInterceptor.InterceptAsync(
                adapter
            );

            return (TResult)adapter.ReturnValue;
        }
    }
}
