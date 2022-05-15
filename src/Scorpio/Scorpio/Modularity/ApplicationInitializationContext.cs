using System;
using System.Collections.Generic;

using Scorpio.DependencyInjection;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInitializationContext:IServiceProviderAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<object> Parameters { get; }

        internal ApplicationInitializationContext(IServiceProvider serviceProvider, params object[] initializeParams)
        {
            Parameters = initializeParams;
            ServiceProvider = serviceProvider;
        }
    }
}
