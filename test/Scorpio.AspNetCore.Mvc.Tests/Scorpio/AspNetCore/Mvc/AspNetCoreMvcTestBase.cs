using System.IO;
using System.Linq;

using Microsoft.Extensions.Hosting;

namespace Scorpio.AspNetCore.Mvc
{
    public abstract class AspNetCoreMvcTestBase : AspNetCoreTestBase<TestModule, Startup>
    {
      
    }
}
