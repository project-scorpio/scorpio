using System.Threading.Tasks;

namespace Scorpio.EventBus.TestClasses
{
    internal class EmptyEventHandler : IEventHandler<string>
    {
        public Task HandleEventAsync(string eventData)
        {
            return Task.CompletedTask;
        }
    }
}
