using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagHelperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string ToClassName(this Enum @enum) => @enum.ToClassName("{0}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToClassName(this Enum @enum, string format)
        {
            var type = @enum.GetType();
            var members = type.GetMember(@enum.ToString());
            var value = members.FirstOrDefault()?.GetAttribute<ClassNameAttribute>()?.ClassName ?? @enum.ToString().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            return string.Format(format, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string ToTagName(this Enum @enum)
        {
            var type = @enum.GetType();
            var members = type.GetMember(@enum.ToString());
            return members.FirstOrDefault()?.GetAttribute<TagNameAttribute>()?.TagName ?? @enum.ToString().ToLowerInvariant();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClassNameAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        public ClassNameAttribute(string className) => ClassName = className;

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TagNameAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public TagNameAttribute(string tagName) => TagName = tagName;

        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; }
    }

}
