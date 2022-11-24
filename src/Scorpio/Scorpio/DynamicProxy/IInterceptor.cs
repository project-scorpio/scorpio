using System.Threading.Tasks;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
         Task InterceptAsync(IMethodInvocation invocation);
    }
}