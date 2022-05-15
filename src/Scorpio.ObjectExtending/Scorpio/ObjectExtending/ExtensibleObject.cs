using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scorpio.Data;
using Scorpio.DynamicProxy;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExtensibleObject : IHasExtraProperties
    {
        /// <summary>
        /// 
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public ExtensibleObject()
            : this(true)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setDefaultsForExtraProperties"></param>
        public ExtensibleObject(bool setDefaultsForExtraProperties)
        {
            ExtraProperties = new ExtraPropertyDictionary();

            if (setDefaultsForExtraProperties)
            {
                this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
            }
        }

    }
}
