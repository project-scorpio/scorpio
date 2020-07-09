using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;

namespace Scorpio.AspNetCore.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, ITransientDependency
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if (policy != null)
            {
                return policy;
            }
            var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
            policyBuilder.Requirements.Add(new PermissionRequirement(policyName));
            return policyBuilder.Build();
        }
    }
}
