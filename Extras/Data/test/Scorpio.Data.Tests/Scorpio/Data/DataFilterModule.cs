using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.Data
{
    [DependsOn(typeof(DataModule))]
    public class DataFilterEnableModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<DataFilterOptions>(options =>
            {
                options.Configure<ISoftDelete>(c => c.Enable());
            });
            base.ConfigureServices(context);
        }
    }
    [DependsOn(typeof(DataModule))]
    public class DataFilterDisableModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<DataFilterOptions>(options =>
            {
                options.Configure<ISoftDelete>(c => c.Disable());
            });
            base.ConfigureServices(context);
        }
    }
}
