using System;
using System.Collections.Generic;

using Scorpio.Data;
using Scorpio.ObjectExtending;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class EventExecutionErrorContext :ExtensibleObject
    {
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<Exception> Exceptions { get; }

        /// <summary>
        /// 
        /// </summary>
        public object EventData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type EventType { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptions"></param>
        /// <param name="eventType"></param>
        /// <param name="eventBus"></param>
        public EventExecutionErrorContext(List<Exception> exceptions, Type eventType, IEventBus eventBus)
        {
            Exceptions = exceptions;
            EventType = eventType;
            EventBus = eventBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retryAttempt"></param>
        /// <returns></returns>
        public bool TryGetRetryAttempt(out int retryAttempt)
        {
            retryAttempt = 0;
            if (!this.HasProperty(EventErrorHandlerBase.RetryAttemptKey))
            {
                return false;
            }

            retryAttempt = this.GetProperty<int>(EventErrorHandlerBase.RetryAttemptKey);
            return true;

        }
    }
}