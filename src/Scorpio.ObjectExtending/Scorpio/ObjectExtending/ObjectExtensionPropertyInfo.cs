using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectExtensionPropertyInfo : IBasicObjectExtensionPropertyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public ObjectExtensionInfo ObjectExtension { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<Attribute> Attributes { get; }


        /// <summary>
        /// Indicates whether to check the other side of the object mapping
        /// if it explicitly defines the property. This property is used in;
        ///
        /// * .MapExtraPropertiesTo() extension method.
        /// * .MapExtraProperties() configuration for the AutoMapper.
        ///
        /// It this is true, these methods check if the mapping object
        /// has defined the property using the <see cref="ObjectExtensionManager"/>.
        ///
        /// Default: null (unspecified, uses the default logic).
        /// </summary>
        public bool? CheckPairDefinitionOnMapping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<object, object> Configuration { get; }

        /// <summary>
        /// Uses as the default value if <see cref="DefaultValueFactory"/> was not set.
        /// </summary>
        
        public object DefaultValue { get; set; }

        /// <summary>
        /// Used with the first priority to create the default value for the property.
        /// Uses to the <see cref="DefaultValue"/> if this was not set.
        /// </summary>
        
        public Func<object> DefaultValueFactory { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectExtension"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public ObjectExtensionPropertyInfo(
            ObjectExtensionInfo objectExtension,
            Type type,
            string name)
        {
            ObjectExtension = Check.NotNull(objectExtension, nameof(objectExtension));
            Type = Check.NotNull(type, nameof(type));
            Name = Check.NotNull(name, nameof(name));

            Configuration = new Dictionary<object, object>();
            Attributes = new List<Attribute>();

            Attributes.AddRange(ExtensionPropertyHelper.GetDefaultAttributes(Type));
            DefaultValue = TypeHelper.GetDefaultValue(Type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetDefaultValue()
        {
            return ExtensionPropertyHelper.GetDefaultValue(Type, DefaultValueFactory, DefaultValue);
        }
    }
}
