using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Scorpio.Modularity;

namespace Scorpio.BackgroundWorkers
{
    [DependsOn(typeof(BackgroundWorkersModule))]
    public class BackgroundWorkersTestModule:ScorpioModule
    {
        
    }
}
