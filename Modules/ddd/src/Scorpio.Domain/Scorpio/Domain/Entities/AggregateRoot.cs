using System.Collections.Generic;
using System.Collections.ObjectModel;

using Scorpio.Entities;

namespace Scorpio.Domain.Entities
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {

        private readonly Collection<object> _domainEvents = new Collection<object>();
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<object> DomainEvents => _domainEvents;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        protected virtual void AddDomainEvent(object eventData)
        {
            _domainEvents.Add(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {

        private readonly Collection<object> _domainEvents = new Collection<object>();
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<object> DomainEvents => _domainEvents;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        protected virtual void AddDomainEvent(object eventData)
        {
            _domainEvents.Add(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
