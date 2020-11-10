using System;
using System.Linq.Expressions;

namespace Scorpio.Data
{
    internal class FakeFilterContext : IFilterContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParameter<T>() => Activator.CreateInstance<T>();

        public Expression<Func<TEntity, TProperty>> GetPropertyExpression<TEntity, TProperty>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntity), "entity");
            return Expression.Lambda<Func<TEntity, TProperty>>(Expression.Property(param, propertyName), param);
        }
    }
}
