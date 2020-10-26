using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.PreConfigure<DataFilterOptions>(options =>
            {
                options.Configure<ISoftDelete>(f => f.Expression(d => !d.IsDeleted));
            });
            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
        }
    }
}
