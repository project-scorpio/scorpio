using System;
using System.Linq;

using AutoMapper.QueryableExtensions;

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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey>
        : CrudApplicationService<TEntity, TEntityDto, TKey, ListRequest<TEntityDto>>,
        ICrudApplicationService<TEntityDto, TKey>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected CrudApplicationService(IRepository<TEntity, TKey> repository) : base(repository)
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput>
        : CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected CrudApplicationService(IRepository<TEntity, TKey> repository) : base(repository)
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected CrudApplicationService(IRepository<TEntity, TKey> repository) : base(repository)
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
    /// <typeparam name="TUpdateInput"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public abstract class CrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : CrudApplicationServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>,
        ICrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected CrudApplicationService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual TEntityDto Create(TCreateInput input)
        {

            var entity = Mapper.Map<TEntity>(input);
            var result = Repository.Insert(entity);
            return Mapper.Map<TEntityDto>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(TKey id) => Repository.Delete(id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntityDto Get(TKey id) => Mapper.Map<TEntityDto>(Repository.Get(id));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual IPagedResult<TEntityDto> GetList(TGetListInput input)
        {
            var query = GetQuery(Repository);
            query = ApplyFilter(query, input);
            var totalCount = query.Count();
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            return new PagedResult<TEntityDto>(query.ProjectTo<TEntityDto>(Configuration).ToList()) { TotalCount = totalCount };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual TEntityDto Update(TKey id, TUpdateInput input)
        {
            var entity = Repository.Get(id);
            Mapper.Map(input, entity);
            Repository.Update(entity);
            return Mapper.Map<TEntityDto>(entity);
        }
    }
}
