using Scorpio.Application.Dtos;

namespace Scorpio.Application.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey>
        : ICrudApplicationService<TEntityDto, TKey, ListRequest<TEntityDto>>
        where TEntityDto : IEntityDto<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey, in TGetListInput>
        : ICrudApplicationService<TEntityDto, TKey, TGetListInput, TEntityDto>
        where TEntityDto : IEntityDto<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : ICrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntityDto : IEntityDto<TKey>
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    /// <typeparam name="TUpdateInput"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IApplicationService
        where TEntityDto : IEntityDto<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntityDto Get(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IPagedResult<TEntityDto> GetList(TGetListInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TEntityDto Create(TCreateInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        TEntityDto Update(TKey id, TUpdateInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(TKey id);
    }
}
