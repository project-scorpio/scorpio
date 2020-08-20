using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.Modularity;

namespace Scorpio.AspNetCore
{
    [DependsOn(typeof(AspNetCoreModule))]
    public class TestModule:ScorpioModule
    {
    }
}
