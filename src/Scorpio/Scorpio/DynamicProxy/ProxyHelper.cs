using System;
using System.Reflection;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProxyHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static bool IsProxy<T>(this T proxy) where T : class
        {
            var provider = proxy.GetAttribute<IProxyTargetProvider>(true);
            if (provider != null)
            {
                return provider.IsProxy(proxy);
            }
            foreach (var item in ProxyTargetProvider.Default.Providers)
            {
                var target = item.GetTarget(proxy);
                if (target != null)
                {
                    return item.IsProxy(proxy);
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static T UnProxy<T>(this T proxy) where T : class
        {
            var provider = proxy.GetAttribute<IProxyTargetProvider>(true);
            if (provider != null)
            {
                return provider.GetTarget(proxy).As<T>();
            }
            foreach (var item in ProxyTargetProvider.Default.Providers)
            {
                var target = item.GetTarget(proxy);
                if (target != null)
                {
                    return target.As<T>();
                }
            }
            return proxy;
        }
    }
}
