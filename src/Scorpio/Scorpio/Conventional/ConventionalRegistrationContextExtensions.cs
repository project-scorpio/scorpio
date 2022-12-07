using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection.Conventional;
using Scorpio.DynamicProxy;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
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
        public static IConventionalRegistrationContext RegisterConventionalDependencyInject(this IConventionalRegistrationContext context, Action<IConventionalConfiguration<ConventionalDependencyAction>> configureAction) => context.DoConventionalAction(configureAction);

           /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IConventionalRegistrationContext RegisterConventionalInterceptor(
            this IConventionalRegistrationContext context,
            Action<IConventionalConfiguration<ConventionalInterceptorAction>> configureAction)
        {
            context.Services.RegisterConventionalInterceptor(context.Types, configureAction);
            return context;
        }
    }
}
