using System;
using System.Collections.Generic;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class EventBusOptions
    {
        /// <summary>
        /// 
        /// </summary>
        internal ICollection<EventHandlerDescriptor> Handlers { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool EnabledErrorHandle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<Type, bool> ErrorHandleSelector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventBusRetryStrategyOptions RetryStrategyOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void UseRetryStrategy(Action<EventBusRetryStrategyOptions> action = null)
        {
            EnabledErrorHandle = true;
            RetryStrategyOptions = new EventBusRetryStrategyOptions();
            action?.Invoke(RetryStrategyOptions);
        }
        /// <summary>
        /// 
        /// </summary>
        public EventBusOptions() => Handlers = new HashSet<EventHandlerDescriptor>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerDescriptor"></param>
        public void AddHandler(EventHandlerDescriptor handlerDescriptor) => Handlers.Add(handlerDescriptor);

    }
}
