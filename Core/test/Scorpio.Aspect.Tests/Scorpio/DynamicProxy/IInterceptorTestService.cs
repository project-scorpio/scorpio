using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    public interface IInterceptorable
    {

    }
    public interface IInterceptorTestService: IInterceptorable
    {
        [NonAspect]
        bool InterceptorInvoked { get; }
        [NonAspect]
        bool TestInvoked { get; }

        void Test();
    }
    public interface IInterceptorTestService2
    {
        [NonAspect]
        bool InterceptorInvoked { get; }
        [NonAspect]
        bool TestInvoked { get; }

        void Test();
    }
}
