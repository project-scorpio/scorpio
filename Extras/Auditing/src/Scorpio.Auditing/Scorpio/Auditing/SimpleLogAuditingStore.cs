using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        [FromContainer]
        public ILogger<SimpleLogAuditingStore> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SimpleLogAuditingStore()
        {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
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
