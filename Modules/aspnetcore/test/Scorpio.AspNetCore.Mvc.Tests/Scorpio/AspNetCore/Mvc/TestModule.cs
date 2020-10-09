
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Auditing;
using Scorpio.Modularity;

namespace Scorpio.AspNetCore.Mvc
{
    [DependsOn(typeof(AspNetCoreMvcModule))]
    public class TestModule : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.RegisterAssemblyByConvention();
            context.Services.Configure<AuditingOptions>(opt =>
            {
                opt.EnableAuditingController();
                opt.EnableAuditingPage();
                opt.IsEnabled = true;
                opt.IsEnabledForAnonymousUsers = true;
            });
            context.Services.Configure<RazorPagesOptions>(options =>
            {
                options.RootDirectory = "/Scorpio/AspNetCore/Mvc";
            });
            context.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("/Scorpio/AspNetCore/App/Views/{1}/{0}.cshtml");
            });
        }


    }
}
