using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Modularity;
using Scorpio.RabbitMQ;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(EventBusModule))]
    [DependsOn(typeof(RabbitMQModule))]
    public sealed class RabbitMQEventBusModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ReplaceSingleton<IEventBus, RabbitEventBus>();
            context.Services.ReplaceSingleton<IEventErrorHandler, LocalEventErrorHandler>();
        }
    }
}
