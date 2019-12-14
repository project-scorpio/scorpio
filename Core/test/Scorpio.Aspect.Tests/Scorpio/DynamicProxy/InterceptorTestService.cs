using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    public class InterceptorTestService : IInterceptorTestService
    {
        public bool InterceptorInvoked { get; set; }
        public bool TestInvoked { get; set; }
        public virtual void Test()
        {
            TestInvoked = true;
        }
    }
    public class InterceptorTestService2 : IInterceptorTestService2
    {
        public bool InterceptorInvoked { get; set; }
        public bool TestInvoked { get; set; }
        public virtual void Test()
        {
            TestInvoked = true;
        }
    }

    
}
