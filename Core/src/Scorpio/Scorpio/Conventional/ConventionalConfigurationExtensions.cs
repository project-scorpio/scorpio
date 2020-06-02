using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConventionalConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> Where<TAction>(this IConventionalConfiguration<TAction> configuration, Predicate<Type> predicate)
        {
            return configuration.CreateContext().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> CreateContext<TAction>(this IConventionalConfiguration<TAction> configuration)
        {

            var context = (configuration as ConventionalConfiguration<TAction>).GetInternalContext();
            return context;
        }

        internal static IEnumerable<IConventionalContext> GetContexts(this IConventionalConfiguration configuration)
        {
            return (configuration as ConventionalConfiguration).Contexts;
        }
    }
}
