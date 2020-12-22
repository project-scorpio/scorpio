using Scorpio.ObjectMapping;

namespace Scorpio.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class AutoMapperAutoObjectMappingProvider<TContext> : AutoMapperAutoObjectMappingProvider, IAutoObjectMappingProvider<TContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapperAccessor"></param>
        public AutoMapperAutoObjectMappingProvider(IMapperAccessor mapperAccessor) 
            : base(mapperAccessor)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperAutoObjectMappingProvider : IAutoObjectMappingProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public IMapperAccessor MapperAccessor { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapperAccessor"></param>
        public AutoMapperAutoObjectMappingProvider(IMapperAccessor mapperAccessor) => MapperAccessor = mapperAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDestination Map<TSource, TDestination>(object source) => MapperAccessor.Mapper.Map<TDestination>(source);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => MapperAccessor.Mapper.Map(source, destination);
    }
}
