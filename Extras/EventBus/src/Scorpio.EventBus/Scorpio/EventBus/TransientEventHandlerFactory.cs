using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events
    /// by a transient instance object. 
    /// </summary>
    /// <remarks>
    /// This class always creates a new transient instance of handler.
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : TransientEventHandlerFactory
        where THandler : IEventHandler, new()
    {
        public TransientEventHandlerFactory(IHybridServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory, typeof(THandler))
        {
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
        private readonly IHybridServiceScopeFactory _serviceScopeFactory;
        private readonly Type _handlerType;

        public TransientEventHandlerFactory(IHybridServiceScopeFactory serviceScopeFactory, Type handlerType)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _handlerType = handlerType;
        }
        /// <summary>
        /// Creates a new instance of the handler object.
        /// </summary>
        /// <returns>The handler object</returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = _serviceScopeFactory.CreateScope();
            var handler = ActivatorUtilities.CreateInstance(scope.ServiceProvider, _handlerType) as IEventHandler;
            return new EventHandlerDisposeWrapper(handler, () => scope.Dispose());
        }
    }
}
