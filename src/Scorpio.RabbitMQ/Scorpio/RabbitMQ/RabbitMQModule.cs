using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using EasyNetQ;
using EasyNetQ.ConnectionString;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Scorpio.Modularity;

namespace Scorpio.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RabbitMQModule:ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterEasyNetQ(c =>
            {
                var options = c.Resolve<IOptions<RabbitMQOptions>>();
                return c.Resolve<IConnectionStringParser>().Parse(options.Value.ConnectionString);
            });
        }
    }
}
