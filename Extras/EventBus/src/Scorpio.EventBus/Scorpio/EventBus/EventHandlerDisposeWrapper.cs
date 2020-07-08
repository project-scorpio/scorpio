using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class EventHandlerDisposeWrapper : IEventHandlerDisposeWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public IEventHandler EventHandler { get; }

        private readonly Action _disposeAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="disposeAction"></param>
        public EventHandlerDisposeWrapper(IEventHandler eventHandler, Action disposeAction = null)
        {
            _disposeAction = disposeAction;
            EventHandler = eventHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}
