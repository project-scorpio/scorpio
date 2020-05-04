using Scorpio.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Application.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey>
        :IAsyncCrudApplicationService<TEntityDto,TKey,ListRequest<TEntityDto>>
        where TEntityDto:IEntityDto<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public interface IAsyncCrudApplicationService<TEntityDto, in TKey, in TGetListInput>
        :IAsyncCrudApplicationService<TEntityDto,TKey,TGetListInput,TEntityDto>
        where TEntityDto:IEntityDto<TKey>
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
        :IAsyncCrudApplicationService<TEntityDto,TKey,TGetListInput,TCreateInput,TCreateInput>
        where TEntityDto:IEntityDto<TKey>
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
    public interface IAsyncCrudApplicationService<TEntityDto,in TKey,in TGetListInput, in TCreateInput,in TUpdateInput>
        :IApplicationService
        where TEntityDto:IEntityDto<TKey>
    {
        Task<TEntityDto> GetAsync(TKey id, CancellationToken cancellationToken=default);

        Task<IPagedResult<TEntityDto>> GetListAsync(TGetListInput input, CancellationToken cancellationToken = default);

        Task<TEntityDto> CreateAsync(TCreateInput input, CancellationToken cancellationToken = default);

        Task<TEntityDto> UpdateAsync(TKey id, TUpdateInput input, CancellationToken cancellationToken = default);

        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
