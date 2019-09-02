using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataFilterOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataFilterDescriptor<TFilter> Filter<TFilter>(this DataFilterOptions options) where TFilter : class
        {
            return options.Descriptors.GetOrAdd(typeof(TFilter), t => new DataFilterDescriptor<TFilter>()) as DataFilterDescriptor<TFilter>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="options"></param>
        /// <param name="descriptAction"></param>
        /// <returns></returns>
        public static DataFilterOptions Configure<TFilter>(this DataFilterOptions options, Action<DataFilterDescriptor<TFilter>> descriptAction) where TFilter : class
        {
            var descriptor = options.Filter<TFilter>();
            descriptAction(descriptor);
            return options;
        }
    }
}
