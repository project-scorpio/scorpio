using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Guids
{
    /// <summary>
    /// 
    /// </summary>
    public class SequentialGuidGeneratorOptions
    {
        /// <summary>
        /// Default value: <see cref="SequentialGuidType.SequentialAtEnd"/>.
        /// </summary>
        public SequentialGuidType DefaultSequentialGuidType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SequentialGuidGeneratorOptions()
        {
            DefaultSequentialGuidType = SequentialGuidType.SequentialAsBinary;
        }
    }
}
