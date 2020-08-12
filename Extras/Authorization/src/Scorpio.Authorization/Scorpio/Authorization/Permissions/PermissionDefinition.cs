using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionDefinition
    {
        /// <summary>
        /// Unique name of the permission.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName => Parent switch {
            PermissionDefinition p=>$"{p.FullName}.{Name}",
            _=>Name
        };

        /// <summary>
        /// Parent of this permission if one exists.
        /// If set, this permission can be granted only if parent is granted.
        /// </summary>
        public PermissionDefinition Parent { get; private set; }

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
        public IReadOnlyList<PermissionDefinition> Children => _children.ToImmutableList();
        private readonly List<PermissionDefinition> _children;

        /// <summary>
        /// Can be used to get/set custom properties for this permission definition.
        /// </summary>
        public Dictionary<string, object> Properties { get; }

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
        protected internal PermissionDefinition(string name, string displayName = null)
        {
            Name = Check.NotNull(name, nameof(name));
            DisplayName = displayName ?? name;

            Properties = new Dictionary<string, object>();
            _children = new List<PermissionDefinition>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public virtual PermissionDefinition AddChild(string name, string displayName=default)
        {
            return AddChild(name, displayName, p => { });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual PermissionDefinition AddChild(string name, Action<PermissionDefinition> action)
        {
            return AddChild(name, default, action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual PermissionDefinition AddChild(string name, string displayName,Action<PermissionDefinition> action)
        {
            var child = new PermissionDefinition(name, displayName)
            {
                Parent = this
            };
            action(child);
            _children.Add(child);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{nameof(PermissionDefinition)} {FullName}]";
        }
    }
}
