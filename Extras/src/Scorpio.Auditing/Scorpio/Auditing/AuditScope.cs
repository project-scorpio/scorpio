namespace Scorpio.Auditing
{
    internal class AuditScope : IAuditScope
    {
        public AuditScope(AuditInfo  auditInfo)
        {
            Info = auditInfo;
        }

        public AuditInfo Info { get; }
    }
}