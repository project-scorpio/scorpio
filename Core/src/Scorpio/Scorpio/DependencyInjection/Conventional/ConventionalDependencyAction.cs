using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.Conventional;

namespace Scorpio.DependencyInjection.Conventional
{
    class ConventionalDependencyAction : ConventionalActionBase
    {
        private readonly List<Type> _types;

        
        public ConventionalDependencyAction(IConventionalConfiguration configuration, Assembly assembly) : base(configuration)
        {
            _types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition).ToList();
        }

        protected override void Action(IConventionalContext context)
        {

            _types.FindAll(context.GetTypePredicate().Compile()).ForEach(
                t => context.Get<ICollection<IRegisterAssemblyServiceSelector>>("Service").ForEach(
                    selector => selector.Select(t).ForEach(
                    s => context.Services.ReplaceOrAdd(
                        ServiceDescriptor.Describe(s, t, 
                        context.GetOrAdd<IRegisterAssemblyLifetimeSelector>("Lifetime", new LifetimeSelector(ServiceLifetime.Transient)).Select(t)),
                        t.GetAttribute<ReplaceServiceAttribute>()?.ReplaceService??false
                        ))));
        }
    }
}
