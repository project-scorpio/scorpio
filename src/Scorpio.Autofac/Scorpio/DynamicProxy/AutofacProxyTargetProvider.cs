using Castle.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    internal class AutofacProxyTargetProvider : IProxyTargetProvider
    {
        public object GetTarget(object proxy)
        {
            if (!ProxyUtil.IsProxy(proxy))
            {
                return null;
            }
            return ProxyUtil.GetUnproxiedInstance(proxy);

        }

        public bool IsProxy(object proxy) => ProxyUtil.IsProxy(proxy);
    }
}
