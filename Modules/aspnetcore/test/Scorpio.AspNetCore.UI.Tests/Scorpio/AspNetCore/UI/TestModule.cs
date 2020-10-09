using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI
{
    [DependsOn(typeof(AspNetCoreUiModule))]
    public class TestModule : ScorpioModule
    {
    }
}
