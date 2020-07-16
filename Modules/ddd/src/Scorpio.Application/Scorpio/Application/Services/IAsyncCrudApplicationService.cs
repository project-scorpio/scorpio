using System.Threading;
using System.Threading.Tasks;

using Scorpio.Application.Dtos;

namespace Scorpio.Application.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey>
        : IAsyncCrudApplicationService<TEntityDto, TKey, ListRequest<TEntityDto>>
        where TEntityDto : IEntityDto<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey, in TGetListInput>
        : IAsyncCrudApplicationService<TEntityDto, TKey, TGetListInput, TEntityDto>
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
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : IAsyncCrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
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
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IApplicationService
        where TEntityDto : IEntityDto<TKey>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntityDto> GetAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPagedResult<TEntityDto>> GetListAsync(TGetListInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntityDto> CreateAsync(TCreateInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntityDto> UpdateAsync(TKey id, TUpdateInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
