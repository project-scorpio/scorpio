using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release
    /// handlers using Ioc.
    /// </summary>
    public class IocEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public Type HandlerType { get; }

        private readonly IHybridServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="handlerType"></param>
        public IocEventHandlerFactory(IHybridServiceScopeFactory serviceScopeFactory, Type handlerType)
        {
            _serviceScopeFactory = serviceScopeFactory;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// </summary>
        /// <returns>Resolved handler object</returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return new EventHandlerDisposeWrapper(
                (IEventHandler)scope.ServiceProvider.GetRequiredService(HandlerType), () => scope.Dispose()
            );
        }
    }
}
