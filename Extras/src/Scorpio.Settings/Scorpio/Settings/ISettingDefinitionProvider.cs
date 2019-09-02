using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingDefinitionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Define(ISettingDefinitionContext context);
    }
}
