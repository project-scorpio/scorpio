using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class SettingStoreContext : ISettingStoreContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        public SettingDefinition SettingDefinition { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinition"></param>
        public SettingStoreContext(SettingDefinition settingDefinition)
        {
            SettingDefinition = settingDefinition;
        }
    }
}
