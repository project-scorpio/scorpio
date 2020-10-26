using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.Setting
{
    [DependsOn(typeof(SettingModule))]
    public class SettingTestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<SettingOptions>(opts =>
            {
                opts.DefinitionProviders.Add<TestSettingDefinitionProvider>();
            });
        }
    }
}
