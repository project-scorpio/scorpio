using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DynamicProxy
{
    internal class ProxyConventionalAction : IProxyConventionalAction
    {
        public void Action(IProxyConventionalActionContext context)
        {
            var predicate = context.TypePredicate.Compile();
            var interceptors = context.Services.GetSingletonInstanceOrAdd(s => new ServiceInterceptorList());
            context.Interceptors.ForEach(t=>context.Services.AddTransient(t));
            context.Types.Where(predicate).ForEach(t =>
            {
                interceptors.Add(t, context.Interceptors);
            });

        }
    }
}
