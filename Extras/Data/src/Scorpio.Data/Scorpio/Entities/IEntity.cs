using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Entities
{
    /// <summary>
    /// Defines an entity. It's primary key may not be "Id" or it may have a composite primary key.
    /// Use <see cref="IEntity{TPrimaryKey}"/> where possible for better integration to repositories and other structures in the framework.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object[] GetKeys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsTransient();

    }

    /// <summary>
    /// Defines an entity with a single primary key with "Id" property.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TPrimaryKey>:IEntity
    {

        /// <summary>
        /// 
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
