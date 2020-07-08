using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

namespace Scorpio.EventBus.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EventHandlerConventionalAction : ConventionalActionBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public EventHandlerConventionalAction(IConventionalConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Action(IConventionalContext context)
        {
            context.Types.ForEach(
                        t => context.Services.Configure<EventBusOptions>(options => options.Handlers.Add(EventHandlerDescriptor.Describe(t, context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").Select(t)))));
        }
    }
}
