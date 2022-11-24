using System.Collections.Generic;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleContainer
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<IModuleDescriptor> Modules { get; }

    }
}
