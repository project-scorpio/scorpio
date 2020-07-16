using System;
using System.Linq.Expressions;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataFilterDescriptorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IDataFilterDescriptor<TFilter> Enable<TFilter>(this IDataFilterDescriptor<TFilter> descriptor) where TFilter : class
        {
            (descriptor as DataFilterDescriptor<TFilter>).IsEnabled = true;
            return descriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IDataFilterDescriptor<TFilter> Disable<TFilter>(this IDataFilterDescriptor<TFilter> descriptor) where TFilter : class
        {
            (descriptor as DataFilterDescriptor<TFilter>).IsEnabled = false;
            return descriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="descriptor"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IDataFilterDescriptor<TFilter> Expression<TFilter>(this IDataFilterDescriptor<TFilter> descriptor, Expression<Func<TFilter, bool>> expression) where TFilter : class
        {
            (descriptor as DataFilterDescriptor<TFilter>).FilterExpression = expression;
            return descriptor;
        }

    }
}
