
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;
using Scorpio;
using System.Reflection;

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
            var action = context.Services.GetSingletonInstanceOrNull<IProxyConventionalAction>();
            if (action == null)
            {
                return;
            }
            var typeList = context.GetOrDefault(Interceptors, default(ITypeList<IInterceptor>));
            var ctx=new ProxyConventionalActionContext(context.Services,context.Types.Where(t=>t.IsStandardType()),context.TypePredicate,typeList);
            action.Action(ctx);
        }
    }
}
