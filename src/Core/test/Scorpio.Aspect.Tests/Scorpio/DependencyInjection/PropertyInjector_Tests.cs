using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.DependencyInjection
{
    public class PropertyInjector_Tests:AspectIntegratedTest<AspectTestModule>
    {
        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            base.SetBootstrapperCreationOptions(options);
        }
        protected override Bootstrapper CreateBootstrapper(IServiceCollection services)
        {
            services.AddTransient<IPropertyService,PropertyService>();
            services.AddTransient<IPropertiesService,PropertiesService>();
            return base.CreateBootstrapper(services);
        }

        [Fact]
        public void Test()
        {
            ServiceProvider.GetRequiredService<IPropertyService>().PropertiesService.ShouldNotBeNull();
        }
    }

    public interface IPropertyService
    {
        IPropertiesService PropertiesService{get;}
    }

    class PropertyService : IPropertyService
    {
        public IPropertiesService PropertiesService { get;set; }
    }

    public interface IPropertiesService
    {

    }

    class PropertiesService : IPropertiesService
    {

    }
}
