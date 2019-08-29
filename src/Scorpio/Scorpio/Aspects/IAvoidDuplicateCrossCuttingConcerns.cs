using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}
