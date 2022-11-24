using System.Collections.Generic;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMQEventBusOptions:EventBusOptions
    {

        /// <summary>
        /// 
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExchangeName { get; set; }
    }
}
