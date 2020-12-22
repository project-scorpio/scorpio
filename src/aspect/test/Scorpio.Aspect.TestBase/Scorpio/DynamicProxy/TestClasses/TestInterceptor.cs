using System.Threading.Tasks;

namespace Scorpio.DynamicProxy.TestClasses
{
    public class TestInterceptor : IInterceptor
    {

        public Task InterceptAsync(IMethodInvocation invocation) => invocation.ProceedAsync();
    }
}
