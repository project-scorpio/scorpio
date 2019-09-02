using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    public interface IDataFilter<TFilter>
        where TFilter : class
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDisposable Enable();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDisposable Disable();

        /// <summary>
        /// 
        /// </summary>
        bool IsEnabled { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDataFilter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        IDisposable Enable<TFilter>()
            where TFilter : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        IDisposable Disable<TFilter>()
            where TFilter : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        bool IsEnabled<TFilter>()
            where TFilter : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsEnabled(Type type);
    }
}
