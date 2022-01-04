using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public sealed class InitializationOrderAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public InitializationOrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Order { get; }

        internal static int GetOrder(Type type,int defaultOrder=0)
        {
            var attr = type.GetAttribute<InitializationOrderAttribute>();
            if (attr==null)
            {
                return defaultOrder;
            }
            return attr.Order;
        }
    }
}
