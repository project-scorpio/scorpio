using AspectCore.DynamicProxy;

using Scorpio.Aspects;

namespace Scorpio
{
    public interface IInterceptorable
    {

    }
    public interface IInterceptorTestService : IInterceptorable, IAvoidDuplicateCrossCuttingConcerns
    {
        [NonAspect]
        bool InterceptorInvoked { get; }

        [NonAspect]
        bool TestInvoked { get; }

        void Test();
    }
    public interface INonInterceptorTestService 
    {
        [NonAspect]
        bool InterceptorInvoked { get; }

        [NonAspect]
        bool TestInvoked { get; }

        void Test();
    }
}
