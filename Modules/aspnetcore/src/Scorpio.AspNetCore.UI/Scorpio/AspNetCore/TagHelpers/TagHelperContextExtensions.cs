using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagHelperContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this TagHelperContext context, string key)
        {
            if (!context.Items.ContainsKey(key))
            {
                return default;
            }

            return (T)context.Items[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T GetValue<T>(this TagHelperContext context )
        {
            var key = typeof(T).FullName;
            return GetValue<T>(context, key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(this TagHelperContext context, string key, object value)
        {
            context.Items[key] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this TagHelperContext context,  T value)
        {
            var key = typeof(T).FullName;
            context.SetValue(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        public static T InitValue<T>(this TagHelperContext context, string key)
            where T : class
        {
            var value = Activator.CreateInstance<T>();
            context.SetValue(key, value);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        public static T InitValue<T>(this TagHelperContext context)
            where T : class
        {
            var key = typeof(T).FullName;
            return context.InitValue<T>(key);
        }
    }
}
