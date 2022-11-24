namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(object obj);
    }
}