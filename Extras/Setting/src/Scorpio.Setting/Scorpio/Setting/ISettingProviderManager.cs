using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingProviderManager
    {
        /// <summary>
        /// 
        /// </summary>
        ICollection<ISettingProvider> Providers { get; }
    }
}
