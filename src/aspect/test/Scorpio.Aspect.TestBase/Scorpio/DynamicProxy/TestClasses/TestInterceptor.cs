using System.Threading.Tasks;

using Scorpio.DynamicProxy;

namespace Scorpio.DynamicProxy.TestClasses
{
    public class TestInterceptor : IInterceptor
    {

        public Task InterceptAsync(IMethodInvocation invocation)
        {

            return invocation.ProceedAsync();
        }
    }
}
