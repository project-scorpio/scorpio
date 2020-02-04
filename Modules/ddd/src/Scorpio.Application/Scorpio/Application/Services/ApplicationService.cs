using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ApplicationService:IApplicationService,ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public static string[] CommonPostfixes { get; set; } = { "AppService", "ApplicationService", "Service" };

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider  ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected ApplicationService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
