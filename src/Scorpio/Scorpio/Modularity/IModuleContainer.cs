using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

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
