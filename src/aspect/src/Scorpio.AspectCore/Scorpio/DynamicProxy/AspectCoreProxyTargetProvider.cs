using System.Linq;
using System.Reflection;

using AspectCore.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    internal class AspectCoreProxyTargetProvider : IProxyTargetProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = " <挂起>")]
        public object GetTarget(object proxy)
        {
            if (!ReflectionUtils.IsProxy(proxy))
            {
                return null;
            }
            var targetField = proxy.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(f => f.Name == "_implementation");
            return targetField?.GetValue(proxy);

        }

        public bool IsProxy(object proxy) => ReflectionUtils.IsProxy(proxy);
    }
}
