using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Scorpio.Modularity;
using Scorpio.TestBase;

namespace Scorpio.AspNetCore.TestBase
{
    public abstract class AspNetCoreIntegratedTestBase<TStartupModule, TStartup> : IntegratedTestBase
        where TStartupModule : IScorpioModule
        where TStartup : class

    {

        protected override void SetBootstrapperCreationOptions(BootstrapperCreationOptions options)
        {
            options.UseAspectCore();
        }
        protected override IBootstrapper Bootstrapper => ServiceProvider.GetService<IBootstrapper>();
        protected TestServer Server { get; }

        protected HttpClient Client { get; }

        public override IServiceProvider ServiceProvider { get;}

        private readonly IHost _host;

        protected AspNetCoreIntegratedTestBase()
        {
            var builder = CreateHostBuilder();

            _host = builder.Build();
            _host.Start();

            Server = _host.GetTestServer();
            Client = _host.GetTestClient();

            ServiceProvider = Server.Services;
        }

        protected virtual IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                    webBuilder.UseTestServer();
                }).AddScorpio<TStartupModule>(SetBootstrapperCreationOptions);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Server.Dispose();
            Client.Dispose();
            _host.Dispose();
        }

        #region GetUrl

        /// <summary>
        /// Gets default URL for given controller type.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        protected virtual string GetUrl<TController>() => "/" + typeof(TController).Name.RemovePostFix("Controller", "AppService", "ApplicationService", "Service");

        /// <summary>
        /// Gets default URL for given controller type's given action.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        protected virtual string GetUrl<TController>(string actionName) => GetUrl<TController>() + "/" + actionName;

        /// <summary>
        /// Gets default URL for given controller type's given action with query string parameters (as anonymous object).
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        protected virtual string GetUrl<TController>(string actionName, object queryStringParamsAsAnonymousObject)
        {
            var url = GetUrl<TController>(actionName);

            var dictionary = new RouteValueDictionary(queryStringParamsAsAnonymousObject);
            if (dictionary.Any())
            {
                url += "?" + dictionary.Select(d => $"{d.Key}={d.Value}").ExpandToString("&");
            }

            return url;
        }

        #endregion

    }
}
