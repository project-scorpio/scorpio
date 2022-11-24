using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.Data
{
    [DependsOn(typeof(DataModule))]
    public class DataFilterEnableModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context) => context.Services.Configure<DataFilterOptions>(options => options.Configure<ISoftDelete>(c => c.Enable()));
    }
    [DependsOn(typeof(DataModule))]
    public class DataFilterDisableModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context) => context.Services.Configure<DataFilterOptions>(options => options.Configure<ISoftDelete>(c => c.Disable()));
    }
}
