using Scorpio.Threading;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuditingStoreExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditingStore"></param>
        /// <param name="auditInfo"></param>
        public static void Save(this IAuditingStore auditingStore, AuditInfo auditInfo) => AsyncHelper.RunSync(() => auditingStore.SaveAsync(auditInfo));

    }
}
