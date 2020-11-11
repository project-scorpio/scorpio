namespace Scorpio.EventBus.TestClasses
{
    public class GenericEventData<T> : IEventDataWithInheritableGenericArgument
    {
        public T Value { get; }

        public GenericEventData(T value) => Value = value;
        public object[] GetConstructorArgs() => new object[] { Value };
    }
}
