using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SettingDefinition GetOrNull(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitions"></param>
        void Add(params SettingDefinition[] definitions);
    }
}
