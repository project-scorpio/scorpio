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
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork => _currentUnitOfWorkProvider?.Current;
        /// <summary>
        /// 
        /// </summary>
        protected IClock Clock { get; }
        /// <summary>
        /// 
        /// </summary>
        protected ILogger Logger { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected DomainService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            _currentUnitOfWorkProvider = serviceProvider.GetService<ICurrentUnitOfWorkProvider>();
            Logger = serviceProvider.GetService<ILoggerFactory>(() => NullLoggerFactory.Instance).CreateLogger(GetType());
            Clock = serviceProvider.GetService<IClock>();
        }
    }
}
