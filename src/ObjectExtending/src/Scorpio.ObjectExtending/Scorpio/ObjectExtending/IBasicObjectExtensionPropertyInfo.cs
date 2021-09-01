using System;
using System.Collections.Generic;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBasicObjectExtensionPropertyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<Attribute> Attributes { get; }

        
        /// <summary>
        /// Uses as the default value if <see cref="DefaultValueFactory"/> was not set.
        /// </summary>
        
        public object DefaultValue { get; set; }

        /// <summary>
        /// Used with the first priority to create the default value for the property.
        /// Uses to the <see cref="DefaultValue"/> if this was not set.
        /// </summary>
        
        public Func<object> DefaultValueFactory { get; set; }
    }
}