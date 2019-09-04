using System;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public  sealed class AuditContributionContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="auditInfo"></param>
        public AuditContributionContext(IServiceProvider serviceProvider, AuditInfo auditInfo)
        {
            ServiceProvider = serviceProvider;
            AuditInfo = auditInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public AuditInfo AuditInfo { get;  }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TWapper"></typeparam>
        /// <returns></returns>
        public TWapper CreateWapper<TWapper>() where TWapper:AuditInfoWapper
            => Activator.CreateInstance(typeof(TWapper), AuditInfo) as TWapper;
    }
}