using Scorpio.DynamicProxy;
using Scorpio.Modularity;

namespace Scorpio.Castle
{
    [DependsOn(typeof(CastleCoreModule))]
    [DependsOn(typeof(AspectTestBaseModule))]
    public class CastleCoreTestModule:ScorpioModule
    {
        public CastleCoreTestModule()
        {
            
        }
    }
}
