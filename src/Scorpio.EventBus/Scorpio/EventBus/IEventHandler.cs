using System.Threading.Tasks;

namespace Scorpio.EventBus
{
    /// <summary>
    /// Undirect base interface for all event handlers.
    /// Implement <see cref="IEventHandler{TEvent}"/> instead of this one.
    /// </summary>
    public interface IEventHandler
    {

    }

    /// <summary>
    /// Defines an interface of a class that handles events asynchrounously of type <see cref="IEventHandler{TEvent}"/>.
    /// </summary>
    /// <typeparam name="TEvent">Event type to handle</typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventData">Event data</param>
        Task HandleEventAsync(object sender, TEvent eventData);
    }
}
