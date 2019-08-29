using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using JetBrains.Annotations;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioModule
    {

        /// <summary>
        /// Before adds services to the container. 
        /// </summary>
        /// <param name="context"></param>
        void PreConfigureServices(ConfigureServicesContext context);

        /// <summary>
        /// Adds services to the container. 
        /// </summary>
        /// <param name="context"></param>
        void ConfigureServices(ConfigureServicesContext context);

        /// <summary>
        /// After adds services to the container. 
        /// </summary>
        /// <param name="context"></param>
        void PostConfigureServices(ConfigureServicesContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PreInitialize(ApplicationInitializationContext context);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Initialize(ApplicationInitializationContext context);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PostInitialize(ApplicationInitializationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Shutdown(ApplicationShutdownContext context);

    }
}
