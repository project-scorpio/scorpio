using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        public static string ToClassName(this Enum @enum)
        {
            var type = @enum.GetType();
            var members = type.GetMember(@enum.ToString());
            return  members.FirstOrDefault()?.GetAttribute<ClassNameAttribute>()?.ClassName ?? @enum.ToString().ToLowerInvariant();
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
        public ClassNameAttribute( string className)
        {
            ClassName = className;
        }

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
        public TagNameAttribute(string tagName)
        {
            TagName = tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; }
    }

}
