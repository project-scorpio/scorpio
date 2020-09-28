using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders.Testing;

using Scorpio.AspNetCore.UI;
using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI.Bootstrap
{
    [DependsOn(typeof(BootstrapModule))]
    public class TestModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSingleton(HtmlEncoder.Default);
        }
    }
}
