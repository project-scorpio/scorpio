using System;

using Scorpio.Conventional;
namespace Scorpio.EventBus.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisterAssemblyEventHandlerContextExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="activationType"></param>
        /// <returns></returns>
        public static IConventionalContext ActivationByType(this IConventionalContext context, EventHandlerActivationType activationType)
        {
            context.Set("HandlerActivationTypeSelector", new ActivationTypeSelector(activationType));
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext AutoActivation(this IConventionalContext context)
        {
            context.Set("HandlerActivationTypeSelector", new ExposeActivationTypeSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IConventionalRegistrationContext RegisterEventHandler(this IConventionalRegistrationContext context, Action<IConventionalConfiguration<EventHandlerConventionalAction>> configureAction) => context.DoConventionalAction(configureAction);


    }
}
