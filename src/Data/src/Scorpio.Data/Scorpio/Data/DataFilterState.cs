﻿namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataFilterState
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnabled"></param>
        public DataFilterState(bool isEnabled) => IsEnabled = isEnabled;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataFilterState Clone() => new DataFilterState(IsEnabled);
    }
}
