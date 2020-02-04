using Microsoft.AspNetCore.Http;
using Scorpio.DependencyInjection;
using Scorpio.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scorpio.AspNetCore.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpContextCancellationTokenProvider : ICancellationTokenProvider,ITransientDependency
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        /// <summary>
        /// 
        /// </summary>
        public CancellationToken Token => _httpContextAccesor.HttpContext?.RequestAborted ?? CancellationToken.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccesor"></param>
        public HttpContextCancellationTokenProvider(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }
    }
}
