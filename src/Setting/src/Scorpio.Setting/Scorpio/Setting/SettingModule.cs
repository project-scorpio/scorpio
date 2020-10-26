using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;
namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<SettingOptions>(opt =>
            {
                opt.SettingProviders.Add<DefaultValueSettingProvider>();
                opt.SettingProviders.Add<DefaultSettingProvider>();
            });
        }
    }
}
