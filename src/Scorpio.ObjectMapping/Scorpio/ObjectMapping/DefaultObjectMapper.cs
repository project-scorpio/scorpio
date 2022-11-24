using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class DefaultObjectMapper<TContext> : DefaultObjectMapper, IObjectMapper<TContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="autoObjectMappingProvider"></param>
        public DefaultObjectMapper(
            IServiceProvider serviceProvider,
            IAutoObjectMappingProvider<TContext> autoObjectMappingProvider
            ) : base(
                serviceProvider,
                autoObjectMappingProvider)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DefaultObjectMapper : IObjectMapper, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public IAutoObjectMappingProvider AutoObjectMappingProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="autoObjectMappingProvider"></param>
        public DefaultObjectMapper(
            IServiceProvider serviceProvider,
            IAutoObjectMappingProvider autoObjectMappingProvider)
        {
            AutoObjectMappingProvider = autoObjectMappingProvider;
            ServiceProvider = serviceProvider;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default;
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var specificMapper = scope.ServiceProvider.GetService<IObjectMapper<TSource, TDestination>>();
                if (specificMapper != null)
                {
                    return specificMapper.Map(source);
                }
            }

            if (source is IMapTo<TDestination> mapperSource)
            {
                return mapperSource.MapTo();
            }

            if (typeof(IMapFrom<TSource>).IsAssignableFrom(typeof(TDestination)))
            {
                try
                {
                    var decType = typeof(TDestination);
                    if (decType.GetConstructors().Select(c => c.GetParameters()).Where(c => c.Length == 1 && (c.SingleOrDefault()?.ParameterType?.IsAssignableFrom(typeof(TSource)) ?? false)).Count() == 1)
                    {
                        return (TDestination)Activator.CreateInstance(typeof(TDestination), source);
                    }
                    else
                    {
                        return ((TDestination)Activator.CreateInstance(typeof(TDestination))).Action(d => (d as IMapFrom<TSource>).MapFrom(source));
                    }
                }
                catch
                {
                    //do nothing.
                }
            }

            return AutoMap<TSource, TDestination>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
            {
                return default;
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var specificMapper = scope.ServiceProvider.GetService<IObjectMapper<TSource, TDestination>>();
                if (specificMapper != null)
                {
                    return specificMapper.Map(source, destination);
                }
            }

            if (source is IMapTo<TDestination> mapperSource)
            {
                mapperSource.MapTo(destination);
                return destination;
            }

            if (destination is IMapFrom<TSource> mapperDestination)
            {
                mapperDestination.MapFrom(source);
                return destination;
            }

            return AutoMap(source, destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual TDestination AutoMap<TSource, TDestination>(object source) => AutoObjectMappingProvider.Map<TSource, TDestination>(source);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected virtual TDestination AutoMap<TSource, TDestination>(TSource source, TDestination destination) => AutoObjectMappingProvider.Map(source, destination);
    }
}