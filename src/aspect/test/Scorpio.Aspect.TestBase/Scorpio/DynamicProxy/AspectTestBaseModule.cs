using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.Modularity;
using Scorpio.TestBase.Scorpio.TestBase;

namespace Scorpio.DynamicProxy
{
    [DependsOn(typeof(TestBaseModule))]
    public class AspectTestBaseModule:ScorpioModule
    {
    }
}
