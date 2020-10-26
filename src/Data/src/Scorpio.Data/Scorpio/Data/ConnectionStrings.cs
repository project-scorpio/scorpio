using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ConnectionStrings : Dictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultConnectionStringName = "Default";

        /// <summary>
        /// 
        /// </summary>
        public ConnectionStrings()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ConnectionStrings(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Default
        {
            get => this.GetOrDefault(DefaultConnectionStringName);
            set => this[DefaultConnectionStringName] = value;
        }
    }
}
