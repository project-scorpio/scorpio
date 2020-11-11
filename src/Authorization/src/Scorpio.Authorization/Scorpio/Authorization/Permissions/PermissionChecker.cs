using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;
using Scorpio.Security;

namespace Scorpio.Authorization.Permissions
{
    internal class PermissionChecker : IPermissionChecker, ISingletonDependency
    {
        protected IReadOnlyList<IPermissionGrantingProvider> GrantingProviders => _lazyProviders.Value;

        protected PermissionOptions Options { get; }
        protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IServiceProvider ServiceProvider { get; }

        private readonly Lazy<List<IPermissionGrantingProvider>> _lazyProviders;

        public PermissionChecker(
            IOptions<PermissionOptions> options,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IPermissionDefinitionManager permissionDefinitionManager,
            IServiceProvider serviceProvider)
        {
            Options = options.Value;
            CurrentPrincipalAccessor = currentPrincipalAccessor;
            PermissionDefinitionManager = permissionDefinitionManager;
            ServiceProvider = serviceProvider;

            _lazyProviders = new Lazy<List<IPermissionGrantingProvider>>(
                () => Options
                    .GrantingProviders.
                    Select(t => serviceProvider.GetService(t) as IPermissionGrantingProvider)
                    .ToList(),
                true);
        }

        public Task<bool> CheckAsync(string name) => CheckAsync(CurrentPrincipalAccessor.Principal, name);

        public async Task<bool> CheckAsync(IPrincipal claimsPrincipal, string name)
        {
            if (GrantingProviders.Count == 0)
            {
                return true;
            }
            var context = new PermissionGrantingContext(
                PermissionDefinitionManager.Get(name),
                claimsPrincipal
                );
            return await GrantingProviders.AnyAsync(async p => (await p.GrantAsync(context)).IsGranted);
        }
    }
}
