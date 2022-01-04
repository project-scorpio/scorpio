using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Scorpio.ObjectExtending;
using System.Reflection;

namespace Scorpio.Data
{

    /// <summary>
    /// 
    /// </summary>
    public static class HasExtraPropertiesExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasProperty(this IHasExtraProperties source, string name)
        {
            return source.ExtraProperties.ContainsKey(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object GetProperty(this IHasExtraProperties source, string name, object defaultValue = null)
        {
            return source.ExtraProperties?.GetOrDefault(name)
                   ?? defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TProperty GetProperty<TProperty>(this IHasExtraProperties source, string name, TProperty defaultValue = default)
        {
            var value = source.GetProperty(name);
            if (value == null)
            {
                return defaultValue;
            }

            if (TypeHelper.IsPrimitiveExtended(typeof(TProperty), includeEnums: true))
            {
                var conversionType = typeof(TProperty);
                if (TypeHelper.IsNullable(conversionType))
                {
                    conversionType = conversionType.GetFirstGenericArgumentIfNullable();
                }

                if (conversionType == typeof(Guid))
                {
                    return (TProperty)TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(value.ToString());
                }

                return (TProperty)Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
            }

            throw new ScorpioException("GetProperty<TProperty> does not support non-primitive types. Use non-generic GetProperty method and handle type casting manually.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TSource SetProperty<TSource>(
            this TSource source,
            string name,
            object value)
            where TSource : IHasExtraProperties
        {

            source.ExtraProperties[name] = value;

            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TSource RemoveProperty<TSource>(this TSource source, string name)
            where TSource : IHasExtraProperties
        {
            source.ExtraProperties.Remove(name);
            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static TSource SetDefaultsForExtraProperties<TSource>(this TSource source, Type objectType = null)
            where TSource : IHasExtraProperties
        {
            if (objectType == null)
            {
                objectType = typeof(TSource);
            }

            var properties = ObjectExtensionManager.Instance
                .GetProperties(objectType);

            foreach (var property in properties)
            {
                if (source.HasProperty(property.Name))
                {
                    continue;
                }

                source.ExtraProperties[property.Name] = property.GetDefaultValue();
            }

            return source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="objectType"></param>
        public static void SetDefaultsForExtraProperties(object source, Type objectType)
        {
            if (!(source is IHasExtraProperties))
            {
                throw new ArgumentException($"Given {nameof(source)} object does not implement the {nameof(IHasExtraProperties)} interface!", nameof(source));
            }

            ((IHasExtraProperties)source).SetDefaultsForExtraProperties(objectType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public static void SetExtraPropertiesToRegularProperties(this IHasExtraProperties source)
        {
            var properties = source.GetType().GetProperties()
                .Where(x => source.ExtraProperties.ContainsKey(x.Name)
                            && x.GetSetMethod(true) != null)
                .ToList();

            foreach (var property in properties)
            {
                property.SetValue(source, source.ExtraProperties[property.Name]);
                source.RemoveProperty(property.Name);
            }
        }
    }
}
