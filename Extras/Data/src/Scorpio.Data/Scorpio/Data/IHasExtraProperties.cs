using System.Collections.Generic;

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
