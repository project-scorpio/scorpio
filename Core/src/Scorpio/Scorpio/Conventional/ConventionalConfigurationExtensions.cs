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
        public static IConventionalContext Where(this IConventionalConfiguration configuration, Predicate<Type> predicate)
        {
            return configuration.CreateContext().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConventionalContext CreateContext(this IConventionalConfiguration configuration)
        {

            var context = (configuration as ConventionalConfiguration).GetContext();
            return context;
        }

        internal static IEnumerable<IConventionalContext> GetContexts(this IConventionalConfiguration configuration)
        {
            return (configuration as ConventionalConfiguration).Contexts;
        }
    }
}
