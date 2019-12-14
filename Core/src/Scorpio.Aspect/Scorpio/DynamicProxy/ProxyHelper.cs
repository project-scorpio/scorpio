using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static object UnProxy(this object proxy)
        {
            if (proxy.IsProxy())
            {
                var targetField = proxy.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(f=>f.Name== "_implementation");
                if (targetField!=null)
                {
                    return targetField.GetValue(proxy);
                }
            }
            return proxy;
        }
    }
}
