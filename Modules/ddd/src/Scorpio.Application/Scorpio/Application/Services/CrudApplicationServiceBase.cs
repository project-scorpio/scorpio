using AutoMapper;
using AutoMapper.QueryableExtensions;
using Scorpio.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Linq.Expressions;
using Scorpio.Repositories;
using Scorpio.Entities;

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
    public abstract class CrudApplicationServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : ApplicationService
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>

    {
        protected CrudApplicationServiceBase(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider)
        {
            Repository = repository;
            Configuration = new MapperConfiguration(ConfigMapper);
            Mapper = new Mapper(Configuration);
        }
        public MapperConfiguration Configuration { get; }
        public IMapper Mapper { get; }
        protected IRepository<TEntity, TKey> Repository { get; }

        protected virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> source, TGetListInput input)
        {
            return input switch
            {
                IFilterRequest filter when !string.IsNullOrWhiteSpace(filter.Where) => source.Where(filter.Where, filter.Parameters),
                _ => source
            };
        }

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IPagingRequest paging && paging.MaxResultCount>0)
            {
                return query.Skip(paging.SkipCount).Take(paging.MaxResultCount);
            }
            return query;
        }

        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input)
        {
            return input switch
            {
                ISortingRequest sorting when !string.IsNullOrWhiteSpace(sorting.Sorting) => query.OrderBy(sorting.Sorting),
                IPagingRequest _ => query.OrderByDescending(t => t.Id),
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

        protected virtual IQueryable<TEntityDto> GetQuery(IQueryable<TEntity> sources)
        {
            return sources.ProjectTo<TEntityDto>(Configuration);
        }
    }
}
