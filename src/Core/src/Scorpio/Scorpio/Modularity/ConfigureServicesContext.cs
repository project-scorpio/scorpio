using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureServicesContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bootstrapper"></param>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal protected ConfigureServicesContext(IBootstrapper bootstrapper, IServiceCollection services, IConfiguration configuration)
        {
            Bootstrapper = bootstrapper;
            Services = services;
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBootstrapper Bootstrapper { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

    }
}
