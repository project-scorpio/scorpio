using System.Collections.Generic;

namespace Scorpio.Options
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ExtensibleOptions
    {
        /// <summary>
        /// 
        /// </summary>
        protected internal IDictionary<string, object> ExtendedOption { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ExtensibleOptions() => ExtendedOption = new Dictionary<string, object>();


    }
}
