using System;
using System.Collections.Generic;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public class ProxyTargetProvider
    {
        private readonly HashSet<IProxyTargetProvider> _providers;
        internal IReadOnlyCollection<IProxyTargetProvider> Providers => _providers;
        private static readonly Lazy<ProxyTargetProvider> _provider=new Lazy<ProxyTargetProvider>(()=>new ProxyTargetProvider(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        /// <summary>
        /// 
        /// </summary>
        public static ProxyTargetProvider Default=>_provider.Value;

        private ProxyTargetProvider() => _providers = new HashSet<IProxyTargetProvider>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public void Add(IProxyTargetProvider provider) => _providers.Add(provider);
    }
}
