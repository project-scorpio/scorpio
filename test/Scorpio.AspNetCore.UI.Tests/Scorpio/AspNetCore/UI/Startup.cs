using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.AspNetCore.UI
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Method intentionally left empty.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app.UseAuditing().Use(async (HttpContext ctx,Func<Task> t) => await ctx.Response.WriteAsync("test"));
    }
}
