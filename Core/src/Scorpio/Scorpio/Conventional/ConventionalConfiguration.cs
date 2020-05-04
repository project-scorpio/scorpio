using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class ConventionalConfiguration:IConventionalConfiguration
    {
        internal ConventionalConfiguration(IServiceCollection services, IEnumerable<Type> types)
        {
            Services = services;
            Types = types;
        }

        public IServiceCollection Services { get; }

        public IEnumerable<Type> Types { get; }

        /// <summary>
        /// 
        /// </summary>
        internal ICollection<ConventionalContext> Contexts { get; } = new List<ConventionalContext>();

        internal IConventionalContext GetContext()
        {
            var context = new ConventionalContext(Services,Types);
            Contexts.Add(context);
            return context;
        }
    }
}
