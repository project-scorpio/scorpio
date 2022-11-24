using System.Collections.Generic;

using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Scorpio.DynamicProxy
{
    internal class ProxyConventionalAction : IProxyConventionalAction
    {
        public void Action(IProxyConventionalActionContext context)
        {
            var predicate=context.TypePredicate.Compile();
            context.Interceptors.ForEach(t =>
            {
                context.Services.TryAddTransient(t);
                var interceptorType=typeof(AspectCoreInterceptorAdapter<>).MakeGenericType(t);
                _ = context.Services.ConfigureDynamicProxy(c => c.Interceptors.AddServiced(interceptorType, m =>  predicate(m.DeclaringType)));
            });

        }
    }
}
