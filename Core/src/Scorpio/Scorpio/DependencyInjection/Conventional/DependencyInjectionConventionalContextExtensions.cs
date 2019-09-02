using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using System;
using System.Collections.Generic;
using System.Text;
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
        public static IConventionalContext Lifetime(this IConventionalContext context, ServiceLifetime serviceLifetime)
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
        public static IConventionalContext Lifetime(this IConventionalContext context, IRegisterAssemblyLifetimeSelector  lifetimeSelector)
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
        public static IConventionalContext As(this IConventionalContext context, IRegisterAssemblyServiceSelector serviceSelector)
        {
            context.GetOrAdd<ICollection< IRegisterAssemblyServiceSelector>>("Service",new HashSet<IRegisterAssemblyServiceSelector>()).Add(serviceSelector);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext As<T>(this IConventionalContext context)
        {
            context.As(new TypeSelector<T>());
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext AsDefault(this IConventionalContext context)
        {
            context.As(new DefaultInterfaceSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext AsSelf(this IConventionalContext context)
        {
            context.As(new SelfSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext AsExposeService(this IConventionalContext context)
        {
            context.As(new ExposeServicesSelector()).Lifetime(new ExposeLifetimeSelector());
            return context;
        }
    }
}
