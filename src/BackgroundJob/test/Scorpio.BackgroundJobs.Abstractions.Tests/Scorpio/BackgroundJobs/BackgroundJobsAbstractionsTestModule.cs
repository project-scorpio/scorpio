using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Scorpio.Modularity;

namespace Scorpio.BackgroundJobs
{
    [DependsOn(typeof(BackgroundJobsAbstractionsModule))]
    public class BackgroundJobsAbstractionsTestModule:ScorpioModule
    {
        
    }
}
