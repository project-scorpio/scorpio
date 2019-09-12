using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SettingValue<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public SettingDefinition Definition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }
    }

}
