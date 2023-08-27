using System;
using System.Collections.Generic;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeDictionary : ITypeDictionary<object, object>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseKeyType"></typeparam>
    /// <typeparam name="TBaseValueType"></typeparam>
    public interface ITypeDictionary<in TBaseKeyType, in TBaseValueType> : IDictionary<Type, Type>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        void Add<TKeyType, TValueType>()
            where TKeyType : TBaseKeyType where TValueType : TBaseValueType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        bool TryAdd<TKeyType, TValueType>()
            where TKeyType : TBaseKeyType where TValueType : TBaseValueType;

        /// <summary>
        /// Checks if a type exists in the list.
        /// </summary>
        /// <typeparam name="TKeyType">Type</typeparam>
        /// <returns></returns>
        bool Contains<TKeyType>() where TKeyType : TBaseKeyType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        bool Remove<TKeyType>() where TKeyType : TBaseKeyType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <returns></returns>
        Type GetOrDefault<TKeyType>() where TKeyType : TBaseKeyType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseValueType"></typeparam>
    public interface ITypeDictionary<in TBaseValueType> : IDictionary<string, Type>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValueType"></typeparam>
        /// <param name="key"></param>
        void Add< TValueType>(string key)
             where TValueType : TBaseValueType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValueType"></typeparam>
        /// <param name="key"></param>
        bool TryAdd<TValueType>(string key)
             where TValueType : TBaseValueType;

        /// <summary>
        /// Checks if a type exists in the list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Type GetOrDefault(string key);
    }

}
