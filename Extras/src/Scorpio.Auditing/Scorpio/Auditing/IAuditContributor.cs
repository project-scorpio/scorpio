namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditContributor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PostContribute(AuditContributionContext context);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PreContribute(AuditContributionContext context);
    }
}