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
        internal protected IDictionary<string, object> ExtendedOption { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ExtensibleOptions()
        {
            ExtendedOption = new Dictionary<string, object>();
        }


    }
}
