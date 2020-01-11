using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
        public static IConventionalRegistrationContext DoConventionalAction<TAction>(this IConventionalRegistrationContext  context, Action<IConventionalConfiguration> configureAction) where TAction : ConventionalActionBase
        {
            context.Services.DoConventionalAction<TAction>(context.Types, configureAction);
            return context;
        }

    }
}
