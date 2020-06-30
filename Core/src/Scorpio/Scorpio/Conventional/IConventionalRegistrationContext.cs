using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Conventional
{
    /// <summary>
    /// Used to pass needed objects on conventional registration process.
    /// </summary>
    public interface IConventionalRegistrationContext
    {

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Type> Types { get; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// </summary>
        IServiceCollection Services { get; }
    }
}