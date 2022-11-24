using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    internal class FackAuditingStore : IAuditingStore
    {
        public AuditInfo Info { get; private set; }
        public Task SaveAsync(AuditInfo info)
        {
            Info = info;
            return Task.CompletedTask;
        }
    }
}
