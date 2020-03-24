using AspectCore.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    internal class SimpleLogAuditingStore : IAuditingStore, ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        public ILogger<SimpleLogAuditingStore> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SimpleLogAuditingStore()
        {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
        }

        public SimpleLogAuditingStore(IServiceProvider  serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<SimpleLogAuditingStore>()
                ??NullLogger<SimpleLogAuditingStore>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public Task SaveAsync(AuditInfo auditInfo)
        {
            Logger.LogInformation(auditInfo.ToString());
            return Task.FromResult(0);
        }
    }
}
