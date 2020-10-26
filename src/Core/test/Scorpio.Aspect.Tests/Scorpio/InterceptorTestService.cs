using System.Collections.Generic;

namespace Scorpio
{
    public class InterceptorTestService : IInterceptorTestService
    {
        public bool InterceptorInvoked { get; set; }
        public bool TestInvoked { get; set; }

        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        public virtual void Test()
        {
            TestInvoked = true;
        }
    }
    public class NonInterceptorTestService : INonInterceptorTestService
    {
        public bool InterceptorInvoked { get; set; }
        public bool TestInvoked { get; set; }
        public virtual void Test()
        {
            TestInvoked = true;
        }

    }


}
