using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
namespace Scorpio.DependencyInjection.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionConventionalContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> Lifetime(this IConventionalContext<ConventionalDependencyAction> context, ServiceLifetime serviceLifetime)
        {
            context.Set("Lifetime", new LifetimeSelector(serviceLifetime));
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="lifetimeSelector"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> Lifetime(this IConventionalContext<ConventionalDependencyAction> context, IRegisterAssemblyLifetimeSelector lifetimeSelector)
        {
            context.Set("Lifetime", lifetimeSelector);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceSelector"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> As(this IConventionalContext<ConventionalDependencyAction> context, IRegisterAssemblyServiceSelector serviceSelector)
        {
            context.GetOrAdd<ICollection<IRegisterAssemblyServiceSelector>>("Service", new HashSet<IRegisterAssemblyServiceSelector>()).Add(serviceSelector);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> As<T>(this IConventionalContext<ConventionalDependencyAction> context)
        {
            context.As(new TypeSelector<T>());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> AsDefault(this IConventionalContext<ConventionalDependencyAction> context)
        {
            context.As(new DefaultInterfaceSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> AsAll(this IConventionalContext<ConventionalDependencyAction> context)
        {
            context.As(new AllInterfaceSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> AsSelf(this IConventionalContext<ConventionalDependencyAction> context)
        {
            context.As(new SelfSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<ConventionalDependencyAction> AsExposeService(this IConventionalContext<ConventionalDependencyAction> context)
        {
            context.As(new ExposeServicesSelector()).Lifetime(new ExposeLifetimeSelector());
            return context;
        }
    }
}
