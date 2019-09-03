using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Data
{
    class FakeFilterContext : IFilterContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParameter<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public Expression<Func<TEntity, TProperty>> GetPropertyExpression<TEntity, TProperty>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntity), "entity");
            return Expression.Lambda<Func<TEntity, TProperty>>(Expression.Property(param, propertyName), param);
        }
    }
}
