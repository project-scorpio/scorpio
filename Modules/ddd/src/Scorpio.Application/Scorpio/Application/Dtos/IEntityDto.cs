using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// Defines common properties for entity based DTOs.
    /// </summary>
    public interface IEntityDto
    {
    }

    /// <summary>
    /// Defines common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityDto<TKey> : IEntityDto
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        TKey Id { get; set; }
    }

}
