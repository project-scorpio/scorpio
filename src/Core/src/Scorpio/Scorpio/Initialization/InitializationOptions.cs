using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InitializationOptions
    {

        /// <summary>
        /// 
        /// </summary>
        public InitializationOptions()
        {
            Initializables = new SortedDictionary<int, ITypeList<IInitializable>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        }

        /// <summary>
        /// 
        /// </summary>
        internal SortedDictionary<int, ITypeList<IInitializable>> Initializables { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="order"></param>
        public void AddInitializable<T>(int order) where T : IInitializable
        {
            Initializables.GetOrAdd(order, i => new TypeList<IInitializable>()).Add<T>();
        }


    }
}
