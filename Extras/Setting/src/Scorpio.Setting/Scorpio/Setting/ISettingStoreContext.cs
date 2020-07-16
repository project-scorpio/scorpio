using System.Collections.Generic;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingStoreContext
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// 
        /// </summary>
        SettingDefinition SettingDefinition { get; }

    }
}
