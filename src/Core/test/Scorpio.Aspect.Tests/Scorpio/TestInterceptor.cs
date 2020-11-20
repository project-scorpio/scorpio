using System.Threading.Tasks;

using Scorpio.DynamicProxy;

namespace Scorpio
{
    internal class TestInterceptor : IInterceptor
    {

        public string ServiceMethodName { get; private set; }

        public Task InterceptAsync(IMethodInvocation invocation)
        {
            ServiceMethodName = invocation.Method.Name;
            if (ServiceMethodName == "Test")
            {
                if (invocation.TargetObject is InterceptorTestService service)
                {
                    service.InterceptorInvoked = true;
                }
                if (invocation.TargetObject is NonInterceptorTestService service2)
                {
                    service2.InterceptorInvoked = true;
                }
            }
            return invocation.ProceedAsync();
        }
    }
}
