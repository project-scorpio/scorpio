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
        IDictionary<string, object> ExtraProperties { get; }
    }
}
