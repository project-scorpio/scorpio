using System.Security.Principal;
using System.Threading;

using Scorpio.DependencyInjection;

namespace Scorpio.Security
{
    /// <summary>
    /// 
    /// </summary>
    internal class ThreadCurrentPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual IPrincipal Principal => Thread.CurrentPrincipal;
    }
}
