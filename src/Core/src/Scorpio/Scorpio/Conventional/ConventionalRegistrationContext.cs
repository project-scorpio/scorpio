using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Conventional
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {

        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Type> Types { get; }

        internal ConventionalRegistrationContext(Assembly assembly, IServiceCollection services)
        {
            Types = assembly.GetTypes();
            Services = services;
        }
    }
}
