namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditingManager
    {
        /// <summary>
        /// 
        /// </summary>
        IAuditScope Current { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAuditSaveHandle BeginScope();

    }
}