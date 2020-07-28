using System;
using System.Linq;
using System.Reflection;

using AspectCore.DynamicProxy;

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = " <挂起>")]
        public static T UnProxy<T>(this T proxy) where T : class
        {
            if (!proxy.IsProxy())
            {
                return proxy;
            }
            var targetField = proxy.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First(f => f.Name == "_implementation");
            return targetField.GetValue(proxy).As<T>();
        }
    }
}
