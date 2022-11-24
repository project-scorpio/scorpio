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
        /// <param name="sender"></param>
        /// <param name="messageId"></param>
        /// <param name="eventData"></param>
        /// <param name="eventType"></param>
        public LocalEventMessage(object sender, Guid messageId, object eventData, Type eventType)
        {
            Sender = sender;
            MessageId = messageId;
            EventData = eventData;
            EventType = eventType;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Sender { get; }

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