using System;
using System.Collections.Generic;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInitializationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider RootServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<object> Parameters { get; }

        internal ApplicationInitializationContext(IServiceProvider serviceProvider,IServiceProvider rootServiceProvider, params object[] initializeParams)
        {
            Parameters = initializeParams;
            ServiceProvider = serviceProvider;
            RootServiceProvider = rootServiceProvider;
        }
    }
}
