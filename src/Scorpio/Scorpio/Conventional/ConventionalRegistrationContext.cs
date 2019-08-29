using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Conventional
{
    class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        public Assembly Assembly { get; }


        public IServiceCollection Services { get; }


        internal ConventionalRegistrationContext(Assembly assembly, IServiceCollection  services )
        {
            Assembly = assembly;
            Services = services;
        }
    }
}
