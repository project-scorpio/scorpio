using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Scorpio.DependencyInjection;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    internal class SimpleLogAuditingStore : IAuditingStore, ISingletonDependency
    {

        /// <summary>
        /// 
        /// </summary>
        public ILogger<SimpleLogAuditingStore> Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        public SimpleLogAuditingStore()
        {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
        }

        public SimpleLogAuditingStore(ILogger<SimpleLogAuditingStore> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task SaveAsync(AuditInfo info)
        {
            Logger.LogInformation(info.ToString());
            return Task.FromResult(0);
        }
    }
}
