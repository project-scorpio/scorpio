using System.Threading.Tasks;

namespace Scorpio.EventBus.TestClasses
{
    public class ServicedEventHandler : IEventHandler<TestEventData>, DependencyInjection.ITransientDependency
    {
        public Task HandleEventAsync(TestEventData eventData)
        {
            return Task.CompletedTask;

        }
    }

    public class TestEventData
    {
        public string Id { get; set; }
    }
}
