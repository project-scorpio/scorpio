using System.Collections.Generic;

namespace Scorpio.Domain.Entities
{

    /// <summary>
    /// 
    /// </summary>
    public interface IGeneratesDomainEvents
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<object> DomainEvents { get; }

        /// <summary>
        /// 
        /// </summary>
        void ClearDomainEvents();

    }
}
