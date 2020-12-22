using System;
using System.Threading.Tasks;

using Castle.DynamicProxy;

namespace Scorpio.Castle.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    internal class CastleMethodInvocationAdapter : CastleMethodInvocationAdapterBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected IInvocationProceedInfo ProceedInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="proceedInfo"></param>
        /// <param name="proceed"></param>
        public CastleMethodInvocationAdapter(IInvocation invocation, IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task ProceedAsync() => await Proceed(Invocation, ProceedInfo);
    }
}
