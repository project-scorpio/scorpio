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
        public static IConventionalContext<TAction> Lifetime<TAction>(this IConventionalContext<TAction> context, ServiceLifetime serviceLifetime)
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
        public static IConventionalContext<TAction> Lifetime<TAction>(this IConventionalContext<TAction> context, IRegisterAssemblyLifetimeSelector  lifetimeSelector)
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
        public static IConventionalContext<TAction> As<TAction>(this IConventionalContext<TAction> context, IRegisterAssemblyServiceSelector serviceSelector)
        {
            context.GetOrAdd<ICollection< IRegisterAssemblyServiceSelector>>("Service",new HashSet<IRegisterAssemblyServiceSelector>()).Add(serviceSelector);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> As<TAction,T>(this IConventionalContext<TAction> context)
        {
            context.As(new TypeSelector<T>());
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> AsDefault<TAction>(this IConventionalContext<TAction> context)
        {
            context.As(new DefaultInterfaceSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> AsSelf<TAction>(this IConventionalContext<TAction> context)
        {
            context.As(new SelfSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IConventionalContext<TAction> AsExposeService<TAction>(this IConventionalContext<TAction> context)
        {
            context.As(new ExposeServicesSelector()).Lifetime(new ExposeLifetimeSelector());
            return context;
        }
    }
}
