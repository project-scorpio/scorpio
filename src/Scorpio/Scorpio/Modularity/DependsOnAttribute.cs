using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity
{
    /// <summary>
    /// Used to define dependencies of a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public Type[] DependedTypes { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependedTypes"></param>
        public DependsOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes ?? new Type[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TModule"></typeparam>
    //public class DependsOnAttribute<TModule> : DependsOnAttribute
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public DependsOnAttribute() : base(typeof(TModule))
    //    {
    //    }
    //}
}
