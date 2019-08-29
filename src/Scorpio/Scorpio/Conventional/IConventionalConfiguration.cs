using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConventionalConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        IServiceCollection Services { get; }

    }
}
