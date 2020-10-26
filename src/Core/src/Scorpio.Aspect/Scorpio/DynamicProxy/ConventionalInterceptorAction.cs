using System.Collections.Generic;
using System.Reflection;

using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Conventional;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConventionalInterceptorAction : ConventionalActionBase
    {
        internal const string Interceptors = "Interceptors";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        internal ConventionalInterceptorAction(IConventionalConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Action(IConventionalContext context)
        {
            var typeList = context.GetOrDefault(Interceptors, default(ITypeList<IInterceptor>));
            var predicate = context.TypePredicate.Compile();
            typeList.ForEach(t =>
            {
                context.Services.TryAddTransient(t);
                context.Services.ConfigureDynamicProxy(c =>
                {
                    c.Interceptors.AddServiced(t, m => !m.AttributeExists<NonAspectAttribute>() && predicate(m.DeclaringType));
                });
            });
        }
    }
}
