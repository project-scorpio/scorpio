using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    internal interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
