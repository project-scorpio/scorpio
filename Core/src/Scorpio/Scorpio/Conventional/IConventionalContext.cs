using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalContext
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceCollection  Services{ get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Type> Types { get; }
    }
}
