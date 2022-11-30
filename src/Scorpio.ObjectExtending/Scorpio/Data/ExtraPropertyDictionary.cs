using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ExtraPropertyDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// 
        /// </summary>
        public ExtraPropertyDictionary()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public ExtraPropertyDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}
