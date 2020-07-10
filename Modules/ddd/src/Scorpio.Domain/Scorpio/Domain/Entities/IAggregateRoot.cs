using Scorpio.Entities;

namespace Scorpio.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAggregateRoot : IEntity, IGeneratesDomainEvents
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
