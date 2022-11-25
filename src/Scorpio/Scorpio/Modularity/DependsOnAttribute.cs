
using System;
using System.Diagnostics.CodeAnalysis;

namespace Scorpio.Modularity
{
    /// <summary>
    /// Used to define dependencies of a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [ExcludeFromCodeCoverage]
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
        public DependsOnAttribute(params Type[] dependedTypes) => DependedTypes = dependedTypes ?? new Type[0];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Type[] GetDependedTypes() => DependedTypes;
    }

#if NET7_0_OR_GREATER

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [ExcludeFromCodeCoverage]
    public sealed class DependsOnAttribute<TModule> : DependsOnAttribute
        where TModule:IScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        public DependsOnAttribute():base(typeof(TModule))
        {

        }
    }

#endif

}
