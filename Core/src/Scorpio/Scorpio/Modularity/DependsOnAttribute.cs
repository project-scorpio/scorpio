
using System;

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

}
