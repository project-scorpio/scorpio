using System;
using System.Collections.Generic;

using Scorpio.Conventional;
using Scorpio.EventBus.Conventional;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventBusServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterEventHandler(this IServiceCollection services, IEnumerable<Type> types, Action<IConventionalConfiguration<EventHandlerConventionalAction>> configureAction)
        {
            return services.DoConventionalAction(types, configureAction);
        }
    }
}
