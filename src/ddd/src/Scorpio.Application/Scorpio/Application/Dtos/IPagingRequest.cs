﻿namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPagingRequest
    {
        /// <summary>
        /// 
        /// </summary>
        int SkipCount { get; }

        /// <summary>
        /// 
        /// </summary>
        int MaxResultCount { get; }
    }
}
