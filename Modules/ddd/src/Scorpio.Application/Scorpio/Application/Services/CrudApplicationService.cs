using Scorpio.Application.Dtos;
using Scorpio.Entities;
using Scorpio.Repositories;
using System;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Scorpio.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey>
        : CrudApplicationService<TEntity, TEntityDto, TKey, ListRequest<TEntityDto>>,
        ICrudApplicationService<TEntityDto, TKey>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected CrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput>
        : CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected CrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected CrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : CrudApplicationServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected CrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }

        public virtual TEntityDto Create(TCreateInput input)
        {

            var entity = Mapper.Map<TEntity>(input);
            Repository.Insert(entity);
            return Mapper.Map<TEntityDto>(entity);
        }

        public virtual void Delete(TKey id)
        {
            Repository.Delete(id);
        }

        public virtual TEntityDto Get(TKey id)
        {
            return Mapper.Map<TEntityDto>(Repository.Get(id));
        }

        public virtual IPagedResult<TEntityDto> GetList(TGetListInput input)
        {
            var query = Repository.AsQueryable();
            query = ApplyFilter(query, input);
            var totalCount = query.Count();
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            return new PagedResult<TEntityDto>(query.ProjectTo<TEntityDto>(Configuration).ToList()) { TotalCount = totalCount };
        }

        public virtual TEntityDto Update(TKey id, TUpdateInput input)
        {
            var entity = Repository.Get(id);
            Mapper.Map(input, entity);
            Repository.Update(entity);
            return Mapper.Map<TEntityDto>(entity);
        }
    }
}
