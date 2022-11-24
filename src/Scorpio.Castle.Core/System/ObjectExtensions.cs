
using Castle.DynamicProxy;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="interface"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public static TInterface GenerateInterfaceProxy<TInterface>(this TInterface @interface, params IInterceptor[] interceptors) where TInterface : class
        {
            var proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateInterfaceProxyWithTargetInterface(@interface, interceptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="class"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public static TClass GenerateClassProxy<TClass>(this TClass  @class, params IInterceptor[] interceptors) where TClass : class
        {
            var proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateClassProxyWithTarget(@class, interceptors);
        }
    }
}
