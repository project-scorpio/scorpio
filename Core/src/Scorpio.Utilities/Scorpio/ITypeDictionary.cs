using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeDictionary:ITypeDictionary<object,object>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseKeyType"></typeparam>
    /// <typeparam name="TBaseValueType"></typeparam>
    public interface ITypeDictionary<in TBaseKeyType,in TBaseValueType>:IDictionary<Type,Type>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        void Add<TKeyType, TValueType>() 
            where TKeyType:TBaseKeyType where TValueType:TBaseValueType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        void TryAdd<TKeyType, TValueType>()
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
        void Remove<TKeyType>() where TKeyType : TBaseKeyType;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <returns></returns>
        Type GetOrDefault<TKeyType>() where TKeyType : TBaseKeyType;
    }
}
