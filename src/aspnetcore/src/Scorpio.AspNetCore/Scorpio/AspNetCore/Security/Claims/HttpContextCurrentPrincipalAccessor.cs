using System.Security.Principal;
using System.Threading;

using Microsoft.AspNetCore.Http;

using Scorpio.DependencyInjection;
using Scorpio.Security;

namespace Scorpio.AspNetCore.Security.Claims
{
    internal class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? Thread.CurrentPrincipal;

        public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
