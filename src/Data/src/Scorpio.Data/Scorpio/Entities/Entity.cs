using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scorpio.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object[] GetKeys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsTransient();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[ENTITY: {GetType().Name}] Keys = {GetKeys().ExpandToString(", ")}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class Entity<TPrimaryKey> : Entity, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Entity()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected Entity(TPrimaryKey id) => Id = id;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object[] GetKeys() => new object[] { Id };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default))
            {
                return true;
            }

            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is not Entity<TPrimaryKey> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            var typeOfThis = GetType().GetTypeInfo();
            var typeOfOther = other.GetType().GetTypeInfo();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Id.GetHashCode());


    }
}
