using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerDisposeWrapper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IEventHandler EventHandler { get; }
    }
}
