using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Scorpio.DependencyInjection;
using Scorpio.Timing;
using Scorpio.Uow;

namespace Scorpio.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DomainService : IDomainService, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork => CurrentUnitOfWorkProvider?.Current;
        /// <summary>
        /// 
        /// </summary>
        protected IClock Clock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected ILogger<DomainService> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected DomainService() => Logger = NullLogger<DomainService>.Instance;
    }
}
