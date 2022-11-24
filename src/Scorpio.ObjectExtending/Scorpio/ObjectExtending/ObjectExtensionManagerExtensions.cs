using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Scorpio.Data;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensionManagerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="objectExtensionManager"></param>
        /// <param name="objectTypes"></param>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static ObjectExtensionManager AddOrUpdateProperty<TProperty>(
            this ObjectExtensionManager objectExtensionManager,
            Type[] objectTypes,
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            return objectExtensionManager.AddOrUpdateProperty(
                objectTypes,
                typeof(TProperty),
                propertyName, configureAction
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="objectExtensionManager"></param>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static ObjectExtensionManager AddOrUpdateProperty<TObject, TProperty>(
            this ObjectExtensionManager objectExtensionManager,
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
            where TObject : IHasExtraProperties
        {
            return objectExtensionManager.AddOrUpdateProperty(
                typeof(TObject),
                typeof(TProperty),
                propertyName,
                configureAction
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectExtensionManager"></param>
        /// <param name="objectTypes"></param>
        /// <param name="propertyType"></param>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static ObjectExtensionManager AddOrUpdateProperty(
            this ObjectExtensionManager objectExtensionManager,
            Type[] objectTypes,
            Type propertyType,
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            Check.NotNull(objectTypes, nameof(objectTypes));

            foreach (var objectType in objectTypes)
            {
                objectExtensionManager.AddOrUpdateProperty(
                    objectType,
                    propertyType,
                    propertyName,
                    configureAction
                );
            }

            return objectExtensionManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectExtensionManager"></param>
        /// <param name="objectType"></param>
        /// <param name="propertyType"></param>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static ObjectExtensionManager AddOrUpdateProperty(
            this ObjectExtensionManager objectExtensionManager,
            Type objectType,
            Type propertyType,
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            Check.NotNull(objectExtensionManager, nameof(objectExtensionManager));

            return objectExtensionManager.AddOrUpdate(
                objectType,
                options =>
                {
                    options.AddOrUpdateProperty(
                        propertyType,
                        propertyName,
                        configureAction
                    );
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectExtensionManager"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static ObjectExtensionPropertyInfo GetPropertyOrNull<TObject>(
            this ObjectExtensionManager objectExtensionManager,
            string propertyName)
        {
            return objectExtensionManager.GetPropertyOrNull(
                typeof(TObject),
                propertyName
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectExtensionManager"></param>
        /// <param name="objectType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static ObjectExtensionPropertyInfo GetPropertyOrNull(
            this ObjectExtensionManager objectExtensionManager,
            Type objectType,
            string propertyName)
        {
            Check.NotNull(objectExtensionManager, nameof(objectExtensionManager));
            Check.NotNull(objectType, nameof(objectType));
            Check.NotNull(propertyName, nameof(propertyName));

            return objectExtensionManager
                .GetOrNull(objectType)?
                .GetPropertyOrNull(propertyName);
        }

        private static readonly ImmutableList<ObjectExtensionPropertyInfo> _emptyPropertyList 
            = new List<ObjectExtensionPropertyInfo>().ToImmutableList();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectExtensionManager"></param>
        /// <returns></returns>
        public static ImmutableList<ObjectExtensionPropertyInfo> GetProperties<TObject>(
            this ObjectExtensionManager objectExtensionManager)
        {
            return objectExtensionManager.GetProperties(typeof(TObject));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectExtensionManager"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static ImmutableList<ObjectExtensionPropertyInfo> GetProperties(
            this ObjectExtensionManager objectExtensionManager,
            Type objectType)
        {
            Check.NotNull(objectExtensionManager, nameof(objectExtensionManager));
            Check.NotNull(objectType, nameof(objectType));

            var extensionInfo = objectExtensionManager.GetOrNull(objectType);
            if (extensionInfo == null)
            {
                return _emptyPropertyList;
            }

            return extensionInfo.GetProperties();
        }
    }
}