using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    
    internal abstract class ConventionalConfiguration : IConventionalConfiguration
    {
        protected ConventionalConfiguration(IServiceCollection services, IEnumerable<Type> types)
        {
            Services = services;
            Types = types;
        }

        public IServiceCollection Services { get; }

        public IEnumerable<Type> Types { get; }

        /// <summary>
        /// 
        /// </summary>
        internal ICollection<IConventionalContext> Contexts { get; } = new List<IConventionalContext>();

        internal abstract IConventionalContext GetContext();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    internal class ConventionalConfiguration<TAction> : ConventionalConfiguration, IConventionalConfiguration<TAction>
    {
        internal ConventionalConfiguration(IServiceCollection services, IEnumerable<Type> types) : base(services, types)
        {
        }

        internal override IConventionalContext GetContext() => GetInternalContext();
        internal IConventionalContext<TAction> GetInternalContext()
        {
            var context = new ConventionalContext<TAction>(Services, Types);
            Contexts.Add(context);
            return context;
        }

    }
}
