using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Scorpio.Data;
using Scorpio.Threading;

namespace Scorpio.EventBus
{
    internal class LocalEventBus : EventBusBase
    {

        /// <summary>
        /// Reference to the Logger.
        /// </summary>
        public ILogger<LocalEventBus> Logger { get;}


        public LocalEventBus(IServiceProvider serviceProvider, IEventErrorHandler errorHandler, IOptions<EventBusOptions> options) : base(serviceProvider, errorHandler, options)
        {
            Logger = serviceProvider.GetService<ILogger<LocalEventBus>>();
        }

        public override async Task PublishAsync(Type eventType, object eventData) => await PublishAsync(new LocalEventMessage(Guid.NewGuid(), eventData, eventType));

        public virtual async Task PublishAsync(LocalEventMessage localEventMessage)
        {
            await TriggerHandlersAsync(localEventMessage.EventType, localEventMessage.EventData, errorContext =>
            {
                errorContext.EventData = localEventMessage.EventData;
                errorContext.SetProperty(nameof(LocalEventMessage.MessageId), localEventMessage.MessageId);
            });
        }
        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                           .Locking(factories => factories.Add(factory));

            return new DisposeAction(() => Unsubscribe(eventType, factory));
        }


        public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
        {
            GetOrCreateHandlerFactories(typeof(TEvent))
                          .Locking(factories => factories.RemoveAll(
                                  factory =>
                                  {
                                      if (!(factory is SingleInstanceHandlerFactory singleInstanceFactory))
                                      {
                                          return false;
                                      }

                                      if (!(singleInstanceFactory.HandlerInstance is ActionEventHandler<TEvent> actionHandler))
                                      {
                                          return false;
                                      }
                                      return actionHandler.Action == action;
                                  }));
        }

        public override void Unsubscribe(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories => factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                    ));
        }

        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory) => GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));

        public override void UnsubscribeAll(Type eventType) => GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());

        protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        private bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            if (handlerType == eventType)
            {
                return true;
            }

            //Should trigger for inherited types
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType) => HandlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());

    }
}
