using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHasExtraProperties
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> ExtraProperties { get; }
    }
}
