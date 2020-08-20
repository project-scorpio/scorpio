using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.Modularity;

namespace Scorpio.Uow
{
    [DependsOn(typeof(UnitOfWorkModule))]
    public class TestModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.RegisterAssemblyByConvention();
        }
    }
}
