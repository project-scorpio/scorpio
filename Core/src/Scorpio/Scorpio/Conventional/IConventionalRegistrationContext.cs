using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Collections.Generic;
using System;

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