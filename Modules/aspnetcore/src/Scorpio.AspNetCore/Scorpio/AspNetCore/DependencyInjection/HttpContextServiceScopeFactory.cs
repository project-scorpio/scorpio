using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.DependencyInjection
{
    [ExposeServices(typeof(IHybridServiceScopeFactory))]
    internal class HttpContextServiceScopeFactory : IHybridServiceScopeFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="serviceScopeFactory"></param>
        public HttpContextServiceScopeFactory(IHttpContextAccessor httpContextAccessor, IServiceScopeFactory serviceScopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public IServiceScope CreateScope()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                return _serviceScopeFactory.CreateScope();
            }
            return new ServiceScope(context.RequestServices);
        }

        class ServiceScope : IServiceScope
        {
            public IServiceProvider ServiceProvider { get; }

            public ServiceScope(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }
            public void Dispose()
            {
            }
        }
    }
}
