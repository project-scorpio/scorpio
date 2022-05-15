using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection
{
    [ExposeServices(
        typeof(IHybridServiceScopeFactory),
        typeof(DefaultServiceScopeFactory)
        )]
    internal class DefaultServiceScopeFactory : IHybridServiceScopeFactory
    {
        protected IServiceScopeFactory Factory { get; }

        public DefaultServiceScopeFactory(IServiceScopeFactory factory) => Factory = factory;

        public IServiceScope CreateScope() => Factory.CreateScope();
    }
}
