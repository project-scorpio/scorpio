using System.Collections.Generic;

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
