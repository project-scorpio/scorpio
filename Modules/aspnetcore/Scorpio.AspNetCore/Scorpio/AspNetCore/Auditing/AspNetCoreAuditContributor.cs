using AspectCore.Injector;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Scorpio.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    internal class AspNetCoreAuditContributor : IAuditContributor
    {
        /// <summary>
        /// 
        /// </summary>
        [FromContainer]
        public ILogger<AspNetCoreAuditContributor> Logger { get; set; }


        public AspNetCoreAuditContributor()
        {
            Logger = NullLogger<AspNetCoreAuditContributor>.Instance;
        }
        public void PreContribute(AuditContributionContext context)
        {
            var httpContext = context.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            if (httpContext == null)
            {
                return;
            }
            var wapper = context.CreateWapper<AspNetCoreAuditInfoWapper>();
            wapper.CurrentUser = httpContext.User?.Identity?.Name?? "Anonymous";
            if (wapper.HttpMethod == null)
            {
                wapper.HttpMethod = httpContext.Request.Method;
            }

            if (wapper.Url == null)
            {
                wapper.Url = BuildUrl(httpContext);
            }

            if (wapper.ClientIpAddress == null)
            {
                wapper.ClientIpAddress = GetClientIpAddress(httpContext);
            }

            if (wapper.BrowserInfo == null)
            {
                wapper.BrowserInfo = GetBrowserInfo(httpContext);
            }
        }

        public void PostContribute(AuditContributionContext context)
        {
            var httpContext = context.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            if (httpContext == null)
            {
                return;
            }
            var wapper = context.CreateWapper<AspNetCoreAuditInfoWapper>();

            if (wapper.HttpStatusCode == default)
            {
                wapper.HttpStatusCode = httpContext.Response.StatusCode;
            }
        }

        protected virtual string GetBrowserInfo(HttpContext httpContext)
        {
            return httpContext?.Request?.Headers?["User-Agent"];
        }


        protected virtual string GetClientIpAddress(HttpContext httpContext)
        {
            try
            {
                return httpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogLevel.Warning);
                return null;
            }
        }

        protected virtual string BuildUrl(HttpContext httpContext)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.Host,
                Path = httpContext.Request.Path.ToString(),
                Query = httpContext.Request.QueryString.ToString()
            };
            return uriBuilder.Uri.AbsoluteUri;
        }

    }
}
