using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;

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
        public virtual IPrincipal Principal => Thread.CurrentPrincipal ;
    }
}
