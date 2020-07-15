using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.EntityFrameworkCore.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.EntityFrameworkCore
{
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public sealed class TestModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScorpioDbContext<TestDbContext>(builder =>
            {     
            });
            context.Services.Configure<ScorpioDbContextOptions>(opt =>
            {
                opt.Configure(c => c.DbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            });
            
            base.ConfigureServices(context);
        }
    }
}
