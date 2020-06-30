using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection.Conventional;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConventionalRegistrationContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAction"></typeparam>
        /// <param name="context"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IConventionalRegistrationContext DoConventionalAction<TAction>(this IConventionalRegistrationContext context, Action<IConventionalConfiguration<TAction>> configureAction) where TAction : ConventionalActionBase
        {
            context.Services.DoConventionalAction(context.Types, configureAction);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IConventionalRegistrationContext RegisterConventionalDependencyInject(this IConventionalRegistrationContext context, Action<IConventionalConfiguration<ConventionalDependencyAction>> configureAction)
        {
            return context.DoConventionalAction(configureAction);
        }

    }
}
