using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RabbitMQ.Client;

using Scorpio.Data;
using Scorpio.Threading;

namespace Scorpio.EventBus
{
    internal class RabbitEventBus : EventBusBase
    {
        private readonly IBus _bus;
        private readonly RabbitMQEventBusOptions _options;
        private IExchange _exchange;
        private IQueue _queue;
        protected ConcurrentDictionary<string, Type> EventTypes { get; }
        /// <summary>
        /// Reference to the Logger.
        /// </summary>
        public ILogger<RabbitEventBus> Logger { get; }

        public IRabbitMqEventDataSerializer Serializer { get; }

        public RabbitEventBus(IServiceProvider serviceProvider, IBus bus, IEventErrorHandler errorHandler, IRabbitMqEventDataSerializer serializer, IOptions<RabbitMQEventBusOptions> options) : base(serviceProvider, errorHandler, options)
        {
            Logger = serviceProvider.GetService<ILogger<RabbitEventBus>>();
            _bus = bus;
            Serializer = serializer;
            _options = options.Value;
            EventTypes = new ConcurrentDictionary<string, Type>();
        }
        public override void Initialize()
        {
            _exchange = _bus.Advanced.ExchangeDeclare(_options.ExchangeName, c => c.AsDurable(true).WithType("direct"));
            _queue = _bus.Advanced.QueueDeclare(_options.ClientName, c => c.AsDurable(true).AsExclusive(false).AsAutoDelete(false));
            _bus.Advanced.Consume(_queue, ProcessEventAsync);
            base.Initialize();
        }

        private async Task<AckStrategy> ProcessEventAsync(byte[] buffer,MessageProperties  messageProperties,MessageReceivedInfo messageReceivedInfo)
        {
            var eventName = messageReceivedInfo.RoutingKey;
            var eventType = EventTypes.GetOrDefault(eventName);
            if (eventType == null)
            {
                return AckStrategies.Ack;
            }
            var eventData = Serializer.Deserialize(buffer, eventType);
            await TriggerHandlersAsync(eventType, eventData, errorContext =>
            {
                var retryAttempt = 0;
                if (messageProperties.Headers != null &&
                    messageProperties.Headers.ContainsKey(EventErrorHandlerBase.RetryAttemptKey))
                {
                    retryAttempt = (int)messageProperties.Headers[EventErrorHandlerBase.RetryAttemptKey];
                }
                errorContext.EventData = eventData;
                errorContext.SetProperty(EventErrorHandlerBase.HeadersKey, messageProperties);
                errorContext.SetProperty(EventErrorHandlerBase.RetryAttemptKey, retryAttempt);
            });
            return AckStrategies.Ack;
        }

        public override async Task PublishAsync(Type eventType, object eventData)
        {
            await PublishAsync(eventType, eventData, null);
        }

        public async Task PublishAsync(Type eventType, object eventData, MessageProperties properties, Dictionary<string, object> headersArguments = null)
        {
            var eventName = EventNameAttribute.GetNameOrDefault(eventType);
            var body = Serializer.Serialize(eventData);
            if (properties == null)
{
                properties = new MessageProperties
                {
                    DeliveryMode = 2,
                    MessageId = Guid.NewGuid().ToString("N")
                };
            }
            SetEventMessageHeaders(properties, headersArguments);
            await _bus.Advanced.PublishAsync(_exchange, eventName, true, properties, body);
        }

        private void SetEventMessageHeaders(MessageProperties properties, Dictionary<string, object> headersArguments)
        {
            if (headersArguments == null)
            {
                return;
            }

            properties.Headers ??= new Dictionary<string, object>();

            foreach (var header in headersArguments)
            {
                properties.Headers[header.Key] = header.Value;
            }
        }

        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                           .Locking(factories =>
                           {
                               factories.Add(factory);
                               if (factories.Count == 1)
                               {
                                   _bus.Advanced.Bind(_exchange, _queue, EventNameAttribute.GetNameOrDefault(eventType));
                               }
                               return factories;
                           });

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

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return HandlerFactories.GetOrAdd(eventType, (type) =>
            {
                var eventName = EventNameAttribute.GetNameOrDefault(type);
                EventTypes[eventName] = type;
                return new List<IEventHandlerFactory>();
            });
        }
    }
}
