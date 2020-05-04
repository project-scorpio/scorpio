using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
namespace Scorpio.DynamicProxy
{
    class TestInterceptor : AbstractInterceptor
    {
        public string ServiceMethodName { get; private set; }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            
            ServiceMethodName = context.ServiceMethod.Name;
            if (context.ServiceMethod.Name == "Test")
            {
                if (context.Implementation is InterceptorTestService service)
                {
                    service.InterceptorInvoked = true;
                }
                if (context.Implementation is InterceptorTestService2 service2)
                {
                    service2.InterceptorInvoked = true;
                }
            }
            await next(context);
        }
    }
}
