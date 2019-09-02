using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBootstrapper:IDisposable
    {
        /// <summary>
        /// Type of the startup (entrance) module of the application.
        /// </summary>
        Type StartupModuleType { get; }

        /// <summary>
        /// List of services registered to this application.
        /// Can not add new services to this collection after application initialize.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        IDictionary<string,object> Properties { get; }

        /// <summary>
        /// Reference to the root service provider used by the application.
        /// This can not be used before initialize the application.
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Used to gracefully shutdown the application and all modules.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 
        /// </summary>
        void Initialize(params object[] initializeParams);
    }
}
