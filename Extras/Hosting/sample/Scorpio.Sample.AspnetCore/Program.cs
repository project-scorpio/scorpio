using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Scorpio.Sample.AspnetCore
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).AddScorpio<SampleModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
