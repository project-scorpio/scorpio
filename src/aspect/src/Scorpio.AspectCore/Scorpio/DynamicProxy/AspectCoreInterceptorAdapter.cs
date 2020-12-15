using System.Threading.Tasks;

namespace Scorpio.DynamicProxy
{
    internal class AspectCoreInterceptorAdapter<TInterceptor> : AspectCore.DynamicProxy.AbstractInterceptor
        where TInterceptor : IInterceptor
    {
        private readonly TInterceptor _interceptor;

        public AspectCoreInterceptorAdapter(TInterceptor interceptor) => _interceptor = Check.NotNull(interceptor, nameof(interceptor));
        public override Task Invoke(AspectCore.DynamicProxy.AspectContext context, AspectCore.DynamicProxy.AspectDelegate next)
            => _interceptor.InterceptAsync(new AspectCoreMethodInvocation(Check.NotNull(context, nameof(context)), Check.NotNull(next, nameof(next))));
    }
}
