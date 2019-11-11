using Scorpio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAggregateRoot:IEntity,IGeneratesDomainEvents
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IAggregateRoot, IEntity<TPrimaryKey>
    {

    }
}
