using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        [ExcludeFromCodeCoverage]
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}
