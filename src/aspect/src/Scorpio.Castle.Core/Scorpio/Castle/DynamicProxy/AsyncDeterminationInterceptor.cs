
using Scorpio.DynamicProxy;

using AsyncDeterminationInterceptor = Castle.DynamicProxy.AsyncDeterminationInterceptor;
namespace Scorpio.Castle.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInterceptor"></typeparam>
    public class AsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
       where TInterceptor : IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpInterceptor"></param>
        public AsyncDeterminationInterceptor(TInterceptor abpInterceptor)
            : base(new CastleAsyncInterceptorAdapter(abpInterceptor))
        {

        }
    }
}
