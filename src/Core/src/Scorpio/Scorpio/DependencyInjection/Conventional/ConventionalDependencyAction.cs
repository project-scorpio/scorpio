using System.Collections.Generic;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

namespace Scorpio.DependencyInjection.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConventionalDependencyAction : ConventionalActionBase
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        internal ConventionalDependencyAction(IConventionalConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Action(IConventionalContext context)
        {
            context.Types.ForEach(
                t => context.Get<ICollection<IRegisterAssemblyServiceSelector>>("Service").ForEach(
                    selector => selector.Select(t).ForEach(
                    s => context.Services.ReplaceOrAdd(
                        ServiceDescriptor.Describe(s, t,
                        context.GetOrAdd<IRegisterAssemblyLifetimeSelector>("Lifetime",
                       t => LifetimeSelector.Transient).Select(t)),
                        t.GetAttribute<ReplaceServiceAttribute>()?.ReplaceService ?? false
                        ))));
        }
    }
}
