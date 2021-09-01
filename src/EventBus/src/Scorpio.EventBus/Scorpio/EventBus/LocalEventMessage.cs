using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public  class LocalEventMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="eventData"></param>
        /// <param name="eventType"></param>
        public LocalEventMessage(Guid messageId, object eventData, Type eventType)
        {
            MessageId = messageId;
            EventData = eventData;
            EventType = eventType;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid MessageId { get; }

        /// <summary>
        /// 
        /// </summary>
        public object EventData { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type EventType { get; }
    }
}