using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.AspNetCore.UI;
using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI
{
    [DependsOn(typeof(AspNetCoreUiModule))]
    public class TestModule:ScorpioModule
    {
    }
}
