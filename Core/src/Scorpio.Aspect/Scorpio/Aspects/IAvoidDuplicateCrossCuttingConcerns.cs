using System.Collections.Generic;

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
