using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Guids
{
    /// <summary>
    /// Implements <see cref="IGuidGenerator"/> by using <see cref="Guid.NewGuid"/>.
    /// </summary>
    public class SimpleGuidGenerator : IGuidGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public static SimpleGuidGenerator Instance { get; } = new SimpleGuidGenerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
