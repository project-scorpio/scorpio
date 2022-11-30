using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Scorpio.ObjectExtending
{
    internal static class ExtensionPropertyHelper
    {
        public static IEnumerable<Attribute> GetDefaultAttributes(Type type)
        {
            if (TypeHelper.IsNonNullablePrimitiveType(type) || type.IsEnum)
            {
                yield return new RequiredAttribute();
            }

            if (type.UnWrapNullable().IsEnum)
            {
                yield return new EnumDataTypeAttribute(type);
            }
        }

        public static object GetDefaultValue(
            Type propertyType,
            Func<object> defaultValueFactory = null,
            object defaultValue = null)
        {
            if (defaultValueFactory != null)
            {
                return Convert.ChangeType( defaultValueFactory(),propertyType);
            }

            return Convert.ChangeType(defaultValue ??
                   TypeHelper.GetDefaultValue(propertyType),propertyType);
        }
    }
}