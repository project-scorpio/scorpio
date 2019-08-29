using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection
{
    [ExposeServices(
        typeof(IHybridServiceScopeFactory),
        typeof(DefaultServiceScopeFactory)
        )]
    internal class DefaultServiceScopeFactory : IHybridServiceScopeFactory
    {
        protected IServiceScopeFactory Factory { get; }

        public DefaultServiceScopeFactory(IServiceScopeFactory factory)
        {
            Factory = factory;
        }

        public IServiceScope CreateScope()
        {
            return Factory.CreateScope();
        }
    }
}
