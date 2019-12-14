using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalRegistrar
    {
        /// <summary>
        /// Registers types of given assembly by convention.
        /// </summary>
        /// <param name="context">Registration context</param>
        void Register(IConventionalRegistrationContext context);
    }
}
