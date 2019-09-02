using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Options
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public interface IPreConfigureOptions<in TOptions> where TOptions:class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        void PreConfigure(string name, TOptions options);
    }
}
