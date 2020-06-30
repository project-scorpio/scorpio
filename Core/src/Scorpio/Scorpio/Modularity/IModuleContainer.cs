using System.Collections.Generic;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IModuleContainer
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<IModuleDescriptor> Modules { get; }

    }
}
