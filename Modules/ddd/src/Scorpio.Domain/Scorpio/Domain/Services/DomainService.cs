using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DomainService:IDomainService,ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected DomainService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
