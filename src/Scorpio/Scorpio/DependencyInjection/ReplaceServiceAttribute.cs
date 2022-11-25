using System;
using System.Diagnostics.CodeAnalysis;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ReplaceServiceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ReplaceService { get; set; }
    }
}