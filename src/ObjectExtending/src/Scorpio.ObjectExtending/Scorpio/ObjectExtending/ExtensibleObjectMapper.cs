using System;
using System.Collections.Generic;
using System.Linq;
using Scorpio.Data;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtensibleObjectMapper
    {
        /// <summary>
        /// Copies extra properties from the <paramref name="source"/> object
        /// to the <paramref name="destination"/> object.
        /// 
        /// Checks property definitions (over the <see cref="ObjectExtensionManager"/>)
        /// based on the <paramref name="definitionChecks"/> preference.
        /// </summary>
        /// <typeparam name="TSource">Source class type</typeparam>
        /// <typeparam name="TDestination">Destination class type</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The destination object</param>
        /// <param name="definitionChecks">
        ///     Controls which properties to map.
        /// </param>
        /// <param name="ignoredProperties">Used to ignore some properties</param>
        public static void MapExtraPropertiesTo<TSource, TDestination>(
            TSource source,
            TDestination destination,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
            where TSource : IHasExtraProperties
            where TDestination : IHasExtraProperties
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(destination, nameof(destination));

            ExtensibleObjectMapper.MapExtraPropertiesTo(
                typeof(TSource),
                typeof(TDestination),
                source.ExtraProperties,
                destination.ExtraProperties,
                definitionChecks,
                ignoredProperties
            );
        }

        /// <summary>
        /// Copies extra properties from the <paramref name="sourceDictionary"/> object
        /// to the <paramref name="destinationDictionary"/> object.
        ///
        /// Checks property definitions (over the <see cref="ObjectExtensionManager"/>)
        /// based on the <paramref name="definitionChecks"/> preference.
        /// </summary>
        /// <typeparam name="TSource">Source class type (for definition check)</typeparam>
        /// <typeparam name="TDestination">Destination class type (for definition check)</typeparam>
        /// <param name="sourceDictionary">The source dictionary object</param>
        /// <param name="destinationDictionary">The destination dictionary object</param>
        /// <param name="definitionChecks">
        /// Controls which properties to map.
        /// </param>
        /// <param name="ignoredProperties">Used to ignore some properties</param>
        public static void MapExtraPropertiesTo<TSource, TDestination>(
            Dictionary<string, object> sourceDictionary,
            Dictionary<string, object> destinationDictionary,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
            where TSource : IHasExtraProperties
            where TDestination : IHasExtraProperties
        {
            MapExtraPropertiesTo(
                typeof(TSource),
                typeof(TDestination),
                sourceDictionary,
                destinationDictionary,
                definitionChecks,
                ignoredProperties
            );
        }

        /// <summary>
        /// Copies extra properties from the <paramref name="sourceDictionary"/> object
        /// to the <paramref name="destinationDictionary"/> object.
        /// 
        /// Checks property definitions (over the <see cref="ObjectExtensionManager"/>)
        /// based on the <paramref name="definitionChecks"/> preference.
        /// </summary>
        /// <param name="sourceType">Source type (for definition check)</param>
        /// <param name="destinationType">Destination class type (for definition check)</param>
        /// <param name="sourceDictionary">The source dictionary object</param>
        /// <param name="destinationDictionary">The destination dictionary object</param>
        /// <param name="definitionChecks">
        /// Controls which properties to map.
        /// </param>
        /// <param name="ignoredProperties">Used to ignore some properties</param>
        public static void MapExtraPropertiesTo(
            Type sourceType,
            Type destinationType,
            Dictionary<string, object> sourceDictionary,
            Dictionary<string, object> destinationDictionary,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        {
            Check.AssignableTo<IHasExtraProperties>(sourceType, nameof(sourceType));
            Check.AssignableTo<IHasExtraProperties>(destinationType, nameof(destinationType));
            Check.NotNull(sourceDictionary, nameof(sourceDictionary));
            Check.NotNull(destinationDictionary, nameof(destinationDictionary));

            var sourceObjectExtension = ObjectExtensionManager.Instance.GetOrNull(sourceType);
            var destinationObjectExtension = ObjectExtensionManager.Instance.GetOrNull(destinationType);

            foreach (var keyValue in sourceDictionary)
            {
                if (CanMapProperty(
                    keyValue.Key,
                    sourceObjectExtension,
                    destinationObjectExtension,
                    definitionChecks,
                    ignoredProperties))
                {
                    destinationDictionary[keyValue.Key] = keyValue.Value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="definitionChecks"></param>
        /// <param name="ignoredProperties"></param>
        /// <returns></returns>
        public static bool CanMapProperty<TSource, TDestination>(
            string propertyName,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        {
            return CanMapProperty(
                typeof(TSource),
                typeof(TDestination),
                propertyName,
                definitionChecks,
                ignoredProperties
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="propertyName"></param>
        /// <param name="definitionChecks"></param>
        /// <param name="ignoredProperties"></param>
        /// <returns></returns>
        public static bool CanMapProperty(
            Type sourceType,
            Type destinationType,
            string propertyName,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        {
            Check.AssignableTo<IHasExtraProperties>(sourceType, nameof(sourceType));
            Check.AssignableTo<IHasExtraProperties>(destinationType, nameof(destinationType));
            Check.NotNull(propertyName, nameof(propertyName));

            var sourceObjectExtension = ObjectExtensionManager.Instance.GetOrNull(sourceType);
            var destinationObjectExtension = ObjectExtensionManager.Instance.GetOrNull(destinationType);

            return CanMapProperty(
                propertyName,
                sourceObjectExtension,
                destinationObjectExtension,
                definitionChecks,
                ignoredProperties);
        }

        private static bool CanMapProperty(
            string propertyName,
            ObjectExtensionInfo sourceObjectExtension,
            ObjectExtensionInfo destinationObjectExtension,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        {
            Check.NotNull(propertyName, nameof(propertyName));

            if (ignoredProperties != null &&
                ignoredProperties.Contains(propertyName))
            {
                return false;
            }

            if (definitionChecks != null)
            {
                if (definitionChecks.Value.HasFlag(MappingPropertyDefinitionChecks.Source))
                {
                    if (sourceObjectExtension == null)
                    {
                        return false;
                    }

                    if (!sourceObjectExtension.HasProperty(propertyName))
                    {
                        return false;
                    }
                }

                if (definitionChecks.Value.HasFlag(MappingPropertyDefinitionChecks.Destination))
                {
                    if (destinationObjectExtension == null)
                    {
                        return false;
                    }

                    if (!destinationObjectExtension.HasProperty(propertyName))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                var sourcePropertyDefinition = sourceObjectExtension?.GetPropertyOrNull(propertyName);
                var destinationPropertyDefinition = destinationObjectExtension?.GetPropertyOrNull(propertyName);

                if (sourcePropertyDefinition != null)
                {
                    if (destinationPropertyDefinition != null)
                    {
                        return true;
                    }

                    if (sourcePropertyDefinition.CheckPairDefinitionOnMapping == false)
                    {
                        return true;
                    }
                }
                else if (destinationPropertyDefinition != null)
                {
                    if (destinationPropertyDefinition.CheckPairDefinitionOnMapping == false)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
