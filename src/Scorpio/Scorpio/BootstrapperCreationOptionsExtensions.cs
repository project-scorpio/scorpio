using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public static class BootstrapperCreationOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="action"></param>
        public static void Configuration(this BootstrapperCreationOptions options,Action<IConfigurationBuilder> action)
        {
            options.ConfigurationActions.Add(action);
        }
    }
}
