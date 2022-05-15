using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{

    /// <summary>
    /// 
    /// </summary>
    public class EventBusRetryStrategyOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public int IntervalMillisecond { get; set; } = 3000;

        /// <summary>
        /// 
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;
    }
}
