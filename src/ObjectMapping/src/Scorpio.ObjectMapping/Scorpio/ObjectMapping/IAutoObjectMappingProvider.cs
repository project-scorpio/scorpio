namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAutoObjectMappingProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(object source);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IAutoObjectMappingProvider<TContext> : IAutoObjectMappingProvider
    {

    }
}