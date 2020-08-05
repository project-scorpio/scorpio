using System.Threading.Tasks;

namespace Scorpio.EventBus.TestClasses
{
    internal class EmptyEventHandler : EmptyEventHandler<string>
    {

    }
    internal class EmptyEventHandler<T> : IEventHandler<T>
    {
        public Task HandleEventAsync(T eventData)
        {
            return Task.CompletedTask;
        }
    }
}
