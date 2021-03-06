﻿namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProxyTargetProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        object GetTarget(object proxy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        bool IsProxy(object proxy);
    }
}
