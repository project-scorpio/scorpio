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
        internal Assembly Assembly { get; }


        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Type> Types { get; }

        internal ConventionalRegistrationContext(Assembly assembly, IServiceCollection  services )
        {
            Assembly = assembly;
            Types = assembly.GetTypes();
            Services = services;
        }
    }
}
