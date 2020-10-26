using System.Security.Principal;

namespace Scorpio.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurrentPrincipalAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        IPrincipal Principal { get; }

    }
}
