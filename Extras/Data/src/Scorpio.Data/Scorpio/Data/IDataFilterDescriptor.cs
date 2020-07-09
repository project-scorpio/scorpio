using System;
using System.Linq.Expressions;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataFilterDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// 
        /// </summary>
        Type FilterType { get; }

        /// <summary>
        /// 
        /// </summary>
        IFilterContext FilterContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataFilter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>(IDataFilter dataFilter, IFilterContext context) where TEntity : class;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    public interface IDataFilterDescriptor<TFilter> : IDataFilterDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        Expression<Func<TFilter, bool>> FilterExpression { get; }
    }

}
