using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionDefinitionManager : IPermissionDefinitionManager, ISingletonDependency
    {
        private readonly Lazy<List<IPermissionDefinitionProvider>> _lazyProviders;
        private readonly Lazy<Dictionary<string, PermissionDefinition>> _lazyPermissionDefinitions;
        private readonly Lazy<Dictionary<string, PermissionGroupDefinition>> _lazyPermissionGroupDefinitions;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        protected List<IPermissionDefinitionProvider> Providers => _lazyProviders.Value;

        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, PermissionGroupDefinition> PermissionGroupDefinitions => _lazyPermissionGroupDefinitions.Value;

        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, PermissionDefinition> PermissionDefinitions => _lazyPermissionDefinitions.Value;

        /// <summary>
        /// 
        /// </summary>
        public PermissionOptions Options { get; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        public PermissionDefinitionManager(
            IOptions<PermissionOptions> options,
            IServiceProvider serviceProvider)
        {
            Options = options.Value;
            _serviceProvider = serviceProvider;

            _lazyProviders = new Lazy<List<IPermissionDefinitionProvider>>(CreatePermissionProviders, true);
            _lazyPermissionDefinitions = new Lazy<Dictionary<string, PermissionDefinition>>(CreatePermissionDefinitions, true);
            _lazyPermissionGroupDefinitions = new Lazy<Dictionary<string, PermissionGroupDefinition>>(CreatePermissionGroupDefinitions, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual PermissionDefinition Get(string name)
        {
            var permission = GetOrNull(name);

            if (permission == null)
            {
                throw new PermissionNotFondException(name);
            }

            return permission;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual PermissionDefinition GetOrNull(string name)
        {
            Check.NotNull(name, nameof(name));

            return PermissionDefinitions.GetOrDefault(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<PermissionDefinition> GetPermissions()
        {
            return PermissionDefinitions.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<PermissionGroupDefinition> GetGroups()
        {
            return PermissionGroupDefinitions.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual List<IPermissionDefinitionProvider> CreatePermissionProviders()
        {
            return Options
                .DefinitionProviders
                .Select(p => _serviceProvider.GetRequiredService(p) as IPermissionDefinitionProvider)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, PermissionDefinition> CreatePermissionDefinitions()
        {
            var permissions = new Dictionary<string, PermissionDefinition>();

            foreach (var groupDefinition in PermissionGroupDefinitions.Values)
            {
                foreach (var permission in groupDefinition.Permissions)
                {
                    AddPermissionToDictionaryRecursively(permissions, permission);
                }
            }

            return permissions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="permission"></param>
        protected virtual void AddPermissionToDictionaryRecursively(Dictionary<string, PermissionDefinition> permissions, PermissionDefinition permission)
        {
            if (permissions.ContainsKey(permission.Name))
            {
                throw new ScorpioException("Duplicate permission name: " + permission.Name);
            }

            permissions[permission.Name] = permission;

            foreach (var child in permission.Children)
            {
                AddPermissionToDictionaryRecursively(permissions, child);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, PermissionGroupDefinition> CreatePermissionGroupDefinitions()
        {
            var context = new PermissionDefinitionContext();

            foreach (var provider in Providers)
            {
                provider.Define(context);
            }

            return context.Groups;
        }
    }
}
