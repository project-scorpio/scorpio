
using Microsoft.Extensions.DependencyInjection;

using Scorpio.AutoMapper.TestClasses;
using Scorpio.Modularity;

namespace Scorpio.AutoMapper
{
    [DependsOn(typeof(AutoMapperModule))]
    public class AutoMapperTestModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context) => 
            context.Services.Configure<AutoMapperOptions>(opt=>opt.AddMaps<AutoMapperTestModule>(true))
            .AddAutoMapperObjectMapper<AutoMapperContext>();
    }
}
