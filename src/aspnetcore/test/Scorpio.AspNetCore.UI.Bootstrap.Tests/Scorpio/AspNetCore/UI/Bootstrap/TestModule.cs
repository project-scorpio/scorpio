using System.Text.Encodings.Web;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI.Bootstrap
{
    [DependsOn(typeof(BootstrapModule))]
    public class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context) => context.Services.AddSingleton(HtmlEncoder.Default);
    }
}
