using System;
namespace Scorpio.EventBus.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerActivationTypeSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        EventHandlerActivationType Select(Type handlerType);
    }
}
