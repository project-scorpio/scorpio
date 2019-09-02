using System;
using System.Collections.Generic;
using System.Text;

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
        public ExtensibleOptions()
        {
            ExtendedOption = new Dictionary<string, object>();
        }


    }
}
