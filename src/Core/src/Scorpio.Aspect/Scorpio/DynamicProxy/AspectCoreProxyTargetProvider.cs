using System.Linq;
using System.Reflection;

using AspectCore.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    internal class AspectCoreProxyTargetProvider : IProxyTargetProvider
    {
        public object GetTarget(object proxy)
        {
            if (!proxy.IsProxy())
            {
                return null;
            }
            var targetField = proxy.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(f => f.Name == "_implementation");
            return targetField?.GetValue(proxy);

        }
    }
}
