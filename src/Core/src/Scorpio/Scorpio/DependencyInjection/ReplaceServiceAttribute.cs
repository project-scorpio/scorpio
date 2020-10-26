using System;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ReplaceServiceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ReplaceService { get; set; }
    }
}