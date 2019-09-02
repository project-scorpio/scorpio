using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
        public static DataFilterDescriptor Enable<TFilter>(this DataFilterDescriptor<TFilter> descriptor) where TFilter:class
        {
            descriptor.IsEnabled = true;
            return descriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DataFilterDescriptor Disable<TFilter>(this DataFilterDescriptor<TFilter> descriptor) where TFilter : class
        {
            descriptor.IsEnabled = false;
            return descriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="descriptor"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataFilterDescriptor Expression<TFilter>(this DataFilterDescriptor<TFilter> descriptor, Expression<Func<TFilter, bool>> expression) where TFilter : class
        {
            descriptor.FilterExpression = expression;
            return descriptor;
        }

    }
}
