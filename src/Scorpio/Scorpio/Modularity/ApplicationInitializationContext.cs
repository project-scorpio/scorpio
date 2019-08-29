using System;
using System.Collections.Generic;
using System.Text;

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
        public IEnumerable<object> Parameters { get; }

        internal ApplicationInitializationContext(IServiceProvider serviceProvider, params object[] initializeParams)
        {
            Parameters = initializeParams;
            ServiceProvider = serviceProvider;
        }
    }
}
