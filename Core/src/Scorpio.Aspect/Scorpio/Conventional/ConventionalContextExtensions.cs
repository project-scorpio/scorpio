using System;

using Scorpio.DynamicProxy;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConventionalContextExtensions
    {
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
            return context.DoConventionalAction(configureAction);
        }

    }
}
