using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a transient instance object. 
    /// </summary>
    /// <remarks>
    /// This class always creates a new transient instance of handler.
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// Creates a new instance of the handler object.
        /// </summary>
        /// <returns>The handler object</returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var handler = new THandler();
            return new EventHandlerDisposeWrapper(
                handler,
                () => (handler as IDisposable)?.Dispose()
            );
        }
    }

    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a transient instance object. 
    /// </summary>
    /// <remarks>
    /// This class always creates a new transient instance of handler.
    /// </remarks>
    internal class TransientEventHandlerFactory : IEventHandlerFactory
    {
        private readonly Type _handlerType;

        public TransientEventHandlerFactory(Type handlerType)
        {
            _handlerType = handlerType;
        }
        /// <summary>
        /// Creates a new instance of the handler object.
        /// </summary>
        /// <returns>The handler object</returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var handler = Activator.CreateInstance(_handlerType) as IEventHandler;
            return new EventHandlerDisposeWrapper(handler, () => (handler as IDisposable)?.Dispose());
        }
    }
}
