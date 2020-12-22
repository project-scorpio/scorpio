
using Scorpio.DependencyInjection;

namespace Scorpio.DynamicProxy.TestClasses
{
    public class TestProxiedService : IProxiedService, ITransientDependency
    {
        public void InterfaceMethod(int intValue, string stringValue)
        {
            // Method intentionally left empty.
        }

        public virtual void ProxiedMethod(int intValue, string stringValue)
        {
            // Method intentionally left empty.
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
        public void NonProxiedMethod(int intValue, string stringValue)
        {
            // Method intentionally left empty.
        }
    }
}
