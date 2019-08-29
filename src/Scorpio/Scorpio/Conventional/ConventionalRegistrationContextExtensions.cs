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
        /// <param name="context"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static IConventionalRegistrationContext Action(this IConventionalRegistrationContext context,Action<IConventionalConfiguration> configAction)
        {
            var config = new ConventionalConfiguration(context.Services);
            configAction(config);
            return context;
        }
    }
}
