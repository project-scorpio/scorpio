using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlugInSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type[] GetModules();

    }
}
