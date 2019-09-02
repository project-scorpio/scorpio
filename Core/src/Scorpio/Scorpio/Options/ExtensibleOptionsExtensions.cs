using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Options
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtensibleOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOption"></typeparam>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TOption GetOption<TOption>(this ExtensibleOptions options,string name)
        {
            return (TOption)options.ExtendedOption.GetOrAdd(name,()=>default(TOption));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOption"></typeparam>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="option"></param>
        public static void SetOption<TOption>(this ExtensibleOptions options, string name,TOption option)
        {
             options.ExtendedOption[name]=option;
        }

    }
}
