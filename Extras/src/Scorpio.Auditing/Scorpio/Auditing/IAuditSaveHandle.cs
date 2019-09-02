using System;
using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditSaveHandle: IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        void Save();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

    }
}