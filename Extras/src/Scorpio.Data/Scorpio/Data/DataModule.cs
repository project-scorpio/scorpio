using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

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
                options.Configure<ISoftDelete>(f=>f.Expression(d=>d.IsDeleted==false));
            });
            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
            context.Services.RegisterAssemblyByConventionOfType<DataModule>();
        }
    }
}
