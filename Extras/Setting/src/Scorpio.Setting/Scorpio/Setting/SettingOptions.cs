using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITypeList<ISettingProvider> SettingProviders { get; }

        /// <summary>
        /// 
        /// </summary>
        public SettingOptions()
        {
            DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
            SettingProviders = new TypeList<ISettingProvider>();
        }
    }
}
