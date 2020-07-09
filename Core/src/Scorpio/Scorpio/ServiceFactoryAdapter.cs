using System;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    class ServiceFactoryAdapter<TContainerBuilder> : IServiceFactoryAdapter
    {
        private readonly IServiceProviderFactory<TContainerBuilder> _serviceProviderFactory;


        public ServiceFactoryAdapter(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory ?? throw new ArgumentNullException("serviceProviderFactory");
        }



        public object CreateBuilder(IServiceCollection services)
        {

            return _serviceProviderFactory.CreateBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(object containerBuilder)
        {
            if (_serviceProviderFactory == null)
            {
                throw new InvalidOperationException("CreateBuilder must be called before CreateServiceProvider");
            }
            return _serviceProviderFactory.CreateServiceProvider((TContainerBuilder)containerBuilder);
        }
    }
}

