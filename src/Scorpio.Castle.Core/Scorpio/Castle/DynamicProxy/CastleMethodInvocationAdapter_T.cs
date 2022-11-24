using System;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace Scorpio.Castle.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    internal class CastleMethodInvocationAdapter<TResult> : CastleMethodInvocationAdapterBase
    {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

        public CastleMethodInvocationAdapter(IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync() => ReturnValue = await Proceed(Invocation, ProceedInfo);
    }
}
