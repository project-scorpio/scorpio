using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    class FackAuditingStore : IAuditingStore
    {
        public AuditInfo Info { get; private set; }
        public Task SaveAsync(AuditInfo info)
        {
            Info = info;
            return Task.CompletedTask;
        }
    }
}
