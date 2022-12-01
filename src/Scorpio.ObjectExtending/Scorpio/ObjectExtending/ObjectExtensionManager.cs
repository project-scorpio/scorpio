using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

using Scorpio.Data;

namespace Scorpio.ObjectExtending
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectExtensionManager
    {
        /// <summary>
        /// 
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static ObjectExtensionManager Instance { get; protected set; } = new ObjectExtensionManager();

        /// <summary>
        /// 
        /// </summary>
        [ExcludeFromCodeCoverage]
        public ConcurrentDictionary<object, object> Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected internal ConcurrentDictionary<Type, ObjectExtensionInfo> ObjectsExtensions { get; }

        /// <summary>
        /// 
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected internal ObjectExtensionManager()
        {
            ObjectsExtensions = new ConcurrentDictionary<Type, ObjectExtensionInfo>();
            Configuration = new ConcurrentDictionary<object, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual ObjectExtensionManager AddOrUpdate<TObject>(
            Action<ObjectExtensionInfo> configureAction = null)
        {
            return AddOrUpdate(typeof(TObject), configureAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual ObjectExtensionManager AddOrUpdate(
            Type[] types,
            Action<ObjectExtensionInfo> configureAction = null)
        {
            Check.NotNull(types, nameof(types));

            foreach (var type in types)
            {
                AddOrUpdate(type, configureAction);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public virtual ObjectExtensionManager AddOrUpdate(
            Type type,
            Action<ObjectExtensionInfo> configureAction = null)
        {
            var extensionInfo = ObjectsExtensions.GetOrAdd(
                type,
                _ => new ObjectExtensionInfo(type)
            );

            configureAction?.Invoke(extensionInfo);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public virtual ObjectExtensionInfo GetOrNull<TObject>()
        {
            return GetOrNull(typeof(TObject));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public virtual ObjectExtensionInfo GetOrNull(Type type)
        {
            return ObjectsExtensions.GetOrDefault(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public virtual ImmutableList<ObjectExtensionInfo> GetExtendedObjects()
        {
            return ObjectsExtensions.Values.ToImmutableList();
        }
    }
}
