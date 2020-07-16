using System;
using System.Linq.Expressions;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetParameter<T>();

        /// <summary>
        /// 
        /// </summary>
        Expression<Func<TEntity, TProperty>> GetPropertyExpression<TEntity, TProperty>(string propertyName);
    }
}
