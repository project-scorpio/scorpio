using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizationContext 
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public IStringLocalizerFactory LocalizerFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LocalizationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
        }
    }
}
