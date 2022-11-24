using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 
        /// </summary>

        Assembly Assembly { get; }

        /// <summary>
        /// 
        /// </summary>

        IScorpioModule Instance { get; }

        /// <summary>
        /// 
        /// </summary>

        bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 
        /// </summary>

        IReadOnlyList<IModuleDescriptor> Dependencies { get; }
    }
}