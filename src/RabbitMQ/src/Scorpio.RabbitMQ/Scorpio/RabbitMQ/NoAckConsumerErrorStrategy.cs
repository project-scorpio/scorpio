using System;
using System.Collections.Generic;
using System.Text;

using EasyNetQ;
using EasyNetQ.Consumer;

using RabbitMQ.Client;

namespace Scorpio.RabbitMQ
{
    internal class NoAckConsumerErrorStrategy : DefaultConsumerErrorStrategy
    {
        public NoAckConsumerErrorStrategy(IPersistentConnection connection, ISerializer serializer, IConventions conventions, ITypeNameSerializer typeNameSerializer, IErrorMessageSerializer errorMessageSerializer, ConnectionConfiguration configuration) : base(connection, serializer, conventions, typeNameSerializer, errorMessageSerializer, configuration)
        {
        }

        public override AckStrategy HandleConsumerError(ConsumerExecutionContext context, Exception exception)
        {
            var ex = exception;
            if (ex is AggregateException)
            {
                ex = ex.InnerException;
            }
            if (ex is NoAckWithRequeueException)
            {
                return AckStrategies.NackWithRequeue;
            }
            if (ex is NoAckWithoutRequeueException)
            {
                return AckStrategies.NackWithoutRequeue;
            }
            return base.HandleConsumerError(context, exception);
        }

    }
}
