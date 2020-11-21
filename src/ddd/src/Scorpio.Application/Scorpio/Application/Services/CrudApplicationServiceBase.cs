using System.Linq;
using System.Linq.Dynamic.Core;

using AutoMapper;

using Scorpio.Application.Dtos;
using Scorpio.Entities;
using Scorpio.Repositories;

namespace Scorpio.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    /// <typeparam name="TUpdateInput"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<Pending>")]
    public abstract class CrudApplicationServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : ApplicationService
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected CrudApplicationServiceBase(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
            Configuration = new MapperConfiguration(ConfigMapper);
            Mapper = new Mapper(Configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        public MapperConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IRepository<TEntity, TKey> Repository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> source, TGetListInput input)
        {
            return input switch
            {
                IFilterRequest filter when !string.IsNullOrWhiteSpace(filter.Where) => source.Where(filter.Where, filter.Parameters),
                _ => source
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IPagingRequest paging && paging.MaxResultCount > 0)
            {
                return query.Skip(paging.SkipCount).Take(paging.MaxResultCount);
            }
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input)
        {
            return input switch
            {
                ISortingRequest sorting when !string.IsNullOrWhiteSpace(sorting.Sorting) => query.OrderBy(sorting.Sorting),
                IPagingRequest _ => query.OrderBy(t => t.Id),
                _ => query
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        protected virtual void ConfigMapper(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<TEntity, TEntityDto>();
            expression.CreateMap<TCreateInput, TEntity>();
            expression.CreateMap<TUpdateInput, TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQuery(IQueryable<TEntity> sources) => sources;
    }
}
