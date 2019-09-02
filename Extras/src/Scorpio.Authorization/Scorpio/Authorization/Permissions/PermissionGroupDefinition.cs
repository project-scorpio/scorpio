using System.Collections.Generic;
using System.Collections.Immutable;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionGroupDefinition 
    {
        /// <summary>
        /// Unique name of the group.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Properties { get; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNull(value, nameof(value));
        }
        private string _displayName;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();
        private readonly List<PermissionDefinition> _permissions;

        /// <summary>
        /// Gets/sets a key-value on the <see cref="Properties"/>.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <returns>
        /// Returns the value in the <see cref="Properties"/> dictionary by given <paramref name="name"/>.
        /// Returns null if given <paramref name="name"/> is not present in the <see cref="Properties"/> dictionary.
        /// </returns>
        public object this[string name]
        {
            get => Properties.GetOrDefault(name);
            set => Properties[name] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        protected internal PermissionGroupDefinition(string name, string displayName = null)
        {
            Name = name;
            DisplayName = displayName ?? Name;

            Properties = new Dictionary<string, object>();
            _permissions = new List<PermissionDefinition>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public virtual PermissionGroupDefinition AddPermission(string name, string displayName = null)
        {
            var permission = new PermissionDefinition(name, displayName);

            _permissions.Add(permission);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<PermissionDefinition> GetPermissionsWithChildren()
        {
            var permissions = new List<PermissionDefinition>();

            foreach (var permission in _permissions)
            {
                AddPermissionToListRecursively(permissions, permission);
            }

            return permissions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="permission"></param>
        private void AddPermissionToListRecursively(List<PermissionDefinition> permissions, PermissionDefinition permission)
        {
            permissions.Add(permission);

            foreach (var child in permission.Children)
            {
                AddPermissionToListRecursively(permissions, child);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{nameof(PermissionGroupDefinition)} {Name}]";
        }
    }
}