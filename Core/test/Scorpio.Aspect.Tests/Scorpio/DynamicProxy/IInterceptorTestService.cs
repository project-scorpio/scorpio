using AspectCore.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    public interface IInterceptorable
    {

    }
    public interface IInterceptorTestService : IInterceptorable
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
