
using Scorpio.Modularity;

namespace Scorpio.AspNetCore
{
    [DependsOn(typeof(AspNetCoreModule))]
    public class TestModule : ScorpioModule
    {
    }
}
