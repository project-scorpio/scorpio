using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using Scorpio.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Async;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Authorization.Permissions
{
    class PermissionChecker : IPermissionChecker, ISingletonDependency
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

        public Task<bool> CheckAsync(string name)
        {
            return CheckAsync(CurrentPrincipalAccessor.Principal, name);
        }

        public async Task<bool> CheckAsync(IPrincipal claimsPrincipal, string name)
        {
            var context = new PermissionGrantingContext(
                PermissionDefinitionManager.Get(name),
                claimsPrincipal
                );
            return await GrantingProviders.AnyAsync(async p => (await p.CheckAsync(context)).IsGranted);
        }
    }
}
