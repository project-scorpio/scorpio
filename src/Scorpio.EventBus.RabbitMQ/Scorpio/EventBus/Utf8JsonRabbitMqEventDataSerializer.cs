using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using EasyNetQ;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    [ExcludeFromCodeCoverage]
    internal class Utf8JsonRabbitMqEventDataSerializer : IRabbitMqEventDataSerializer, ISingletonDependency
    {
        private readonly ISerializer _serializer;

        public Utf8JsonRabbitMqEventDataSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }
#if NET5_0_OR_GREATER
        public object Deserialize(ReadOnlyMemory<byte> value, Type type) => _serializer.BytesToMessage(type, value);
        public T Deserialize<T>(ReadOnlyMemory<byte> value) => (T)_serializer.BytesToMessage(typeof(T), value);
        public ReadOnlyMemory<byte> Serialize(object obj) => _serializer.MessageToBytes(obj.GetType(), obj).Memory;

#else
        public object Deserialize(byte[] value, Type type) => _serializer.BytesToMessage(type,value);
        public T Deserialize<T>(byte[] value) => (T)_serializer.BytesToMessage(typeof(T),value);
        public byte[] Serialize(object obj) => _serializer.MessageToBytes(obj.GetType(),obj);
#endif
    }
}
