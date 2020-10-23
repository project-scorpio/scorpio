using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;
namespace Scorpio.EventBus.TestClasses
{
    [DependsOn(typeof(EventBusModule))]
    public class ServicedEventHandlerModule : ScorpioModule
    {
       
    }
}
