using System;

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
