using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Scorpio.Data;

namespace Scorpio.ObjectExtending
{

    /// <summary>
    /// 
    /// </summary>
    public class ObjectExtensionInfo
    {
        
        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ConcurrentDictionary<string, ObjectExtensionPropertyInfo> Properties { get; }

        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<object, object> Configuration { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public ObjectExtensionInfo(Type type)
        {
            Type = Check.NotNull(type, nameof(type));
            Properties = new ConcurrentDictionary<string, ObjectExtensionPropertyInfo>();
            Configuration = new ConcurrentDictionary<object, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual bool HasProperty(string propertyName)
        {
            return Properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual ObjectExtensionInfo AddOrUpdateProperty<TProperty>(
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            return AddOrUpdateProperty(
                typeof(TProperty),
                propertyName,
                configureAction
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="propertyName"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual ObjectExtensionInfo AddOrUpdateProperty(
            Type propertyType,
            string propertyName,
            Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(propertyName, nameof(propertyName));

            var propertyInfo = Properties.GetOrAdd(
                propertyName,
                _ => new ObjectExtensionPropertyInfo(this, propertyType, propertyName)
            );

            configureAction?.Invoke(propertyInfo);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ImmutableList<ObjectExtensionPropertyInfo> GetProperties()
        {
            return Properties.OrderBy(t=>t.Key)
                            .Select(t=>t.Value)
                            .ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual ObjectExtensionPropertyInfo GetPropertyOrNull(
            string propertyName)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));

            return Properties.GetOrDefault(propertyName);
        }
    }
}
