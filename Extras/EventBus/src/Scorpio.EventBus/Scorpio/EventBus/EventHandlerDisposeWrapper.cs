using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    internal class EventHandlerDisposeWrapper : IEventHandlerDisposeWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public IEventHandler EventHandler { get; }

        private readonly Action _disposeAction;
        private bool _disposedValue;

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
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _disposeAction?.Invoke();
                }


                _disposedValue = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
