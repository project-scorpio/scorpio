using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key</typeparam>
    [Serializable]
    public abstract class EntityDto<TKey> : IEntityDto<TKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[DTO: {GetType().Name}] Id = {Id}";
        }
    }
}
