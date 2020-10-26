using System;

namespace Scorpio.Setting
{
    /// <summary>
    /// Defines a setting.
    /// A setting is used to configure and change behavior of the application.
    /// </summary>
    public abstract class SettingDefinition
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Display name of the setting.
        /// This can be used to show setting to the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///  brief description for this setting.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Value type of the setting.
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// Default value of the setting.
        /// </summary>
        public object Default { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="valueType"></param>
        /// <param name="defaultValue"></param>
        protected SettingDefinition(string name, string displayName, string description, Type valueType, object defaultValue)
        {
            this.Name = name;
            this.DisplayName = string.IsNullOrEmpty(displayName) ? name : displayName;
            Description = description;
            ValueType = valueType;
            Default = defaultValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SettingDefinition<T> : SettingDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        public SettingDefinition(string name, T defaultValue = default) : this(name, null, defaultValue: defaultValue)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="defaultValue"></param>
        public SettingDefinition(string name, string displayName, T defaultValue = default) : this(name, displayName, null, defaultValue)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="defaultValue"></param>
        public SettingDefinition(string name, string displayName, string description, T defaultValue = default)
            : base(name, displayName, description, typeof(T), defaultValue)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public new T Default => (T)base.Default;

    }
}
