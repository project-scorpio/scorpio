using System;

using Scorpio.DependencyInjection;

namespace Scorpio.ObjectMapping
{
    internal class NotImplementedAutoObjectMappingProvider<TContext> : NotImplementedAutoObjectMappingProvider, IAutoObjectMappingProvider<TContext>
    {

    }
    /// <summary>
    /// 
    /// </summary>
    internal class NotImplementedAutoObjectMappingProvider : IAutoObjectMappingProvider, ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(object source) => throw new NotImplementedException($"Can not map from given object ({source}) to {typeof(TDestination).AssemblyQualifiedName}.");

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => throw new NotImplementedException($"Can no map from {typeof(TSource).AssemblyQualifiedName} to {typeof(TDestination).AssemblyQualifiedName}.");
    }
}