using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataFilterOptions
    {
        /// <summary>
        /// 
        /// </summary>
        internal Dictionary<Type, DataFilterDescriptor>  Descriptors { get; }

        /// <summary>
        /// 
        /// </summary>
        public DataFilterOptions()
        {
            Descriptors = new Dictionary<Type, DataFilterDescriptor>();
        }
    }
}
