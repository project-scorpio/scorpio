using System;

namespace Scorpio.EventBus.Conventional
{
    internal class ActivationTypeSelector : IEventHandlerActivationTypeSelector
    {
        private readonly EventHandlerActivationType _activationType;

        public ActivationTypeSelector(EventHandlerActivationType activationType) => _activationType = activationType;

        EventHandlerActivationType IEventHandlerActivationTypeSelector.Select(Type handlerType) => _activationType;
    }
}
