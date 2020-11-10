using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    internal class ServiceFactoryAdapter<TContainerBuilder> : IServiceFactoryAdapter
    {
        private readonly IServiceProviderFactory<TContainerBuilder> _serviceProviderFactory;


        public ServiceFactoryAdapter(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory) => _serviceProviderFactory = serviceProviderFactory ?? throw new ArgumentNullException("serviceProviderFactory");



        public object CreateBuilder(IServiceCollection services) => _serviceProviderFactory.CreateBuilder(services);

        public IServiceProvider CreateServiceProvider(object containerBuilder) => _serviceProviderFactory.CreateServiceProvider((TContainerBuilder)containerBuilder);
    }
}

