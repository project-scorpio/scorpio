using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Runtime
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAmbientDataContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetData(string key, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetData(string key);
    }
}
