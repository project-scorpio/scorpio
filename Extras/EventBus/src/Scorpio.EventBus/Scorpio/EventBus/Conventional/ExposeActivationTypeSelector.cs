using System;
using System.Reflection;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus.Conventional
{
    internal class ExposeActivationTypeSelector : IEventHandlerActivationTypeSelector
    {

        EventHandlerActivationType IEventHandlerActivationTypeSelector.Select(Type handlerType)
        {
            if (handlerType.IsAssignableTo<IDependency>())
            {
                return EventHandlerActivationType.ByServiceProvider;
            }
            return EventHandlerActivationType.Transient;
        }
    }
}
