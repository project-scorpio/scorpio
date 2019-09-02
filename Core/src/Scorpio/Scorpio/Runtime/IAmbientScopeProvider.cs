using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Runtime
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAmbientScopeProvider<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey"></param>
        /// <returns></returns>
        T GetValue(string contextKey);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDisposable BeginScope(string contextKey, T value);
    }
}
