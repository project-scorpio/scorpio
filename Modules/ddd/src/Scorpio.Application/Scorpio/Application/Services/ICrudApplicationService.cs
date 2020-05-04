using Scorpio.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey>
        :ICrudApplicationService<TEntityDto,TKey,ListRequest<TEntityDto>>
        where TEntityDto:IEntityDto<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public interface ICrudApplicationService<TEntityDto, in TKey, in TGetListInput>
        :ICrudApplicationService<TEntityDto,TKey,TGetListInput,TEntityDto>
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
    public interface ICrudApplicationService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        :ICrudApplicationService<TEntityDto,TKey,TGetListInput,TCreateInput,TCreateInput>
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
    public interface ICrudApplicationService<TEntityDto,in TKey,in TGetListInput, in TCreateInput,in TUpdateInput>
        :IApplicationService
        where TEntityDto:IEntityDto<TKey>
    {
        TEntityDto Get(TKey id);

        IPagedResult<TEntityDto> GetList(TGetListInput input);

        TEntityDto Create(TCreateInput input);

        TEntityDto Update(TKey id, TUpdateInput input);

        void Delete(TKey id);
    }
}
