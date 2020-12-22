
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectMappingModule : ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context) =>
            context.AddConventionalRegistrar<ObjectMappingConventionalRegistrar>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context) => 
            context.Services.AddTransient(
                typeof(IObjectMapper<>),
                typeof(DefaultObjectMapper<>)
            )
            .AddSingleton(typeof(IAutoObjectMappingProvider<>), typeof(NotImplementedAutoObjectMappingProvider<>));

    }
}
