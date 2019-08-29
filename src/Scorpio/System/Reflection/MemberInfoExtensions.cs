using Scorpio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Reflection
{
    /// <summary>
    /// Extensions to <see cref="MemberInfo"/>.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///  获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DescriptionAttribute"/> 特性描述信息，如不存在则返回 null</returns>
        public static string GetDescription(this MemberInfo member, bool inherit = false)
        {
            var desc = member.GetAttribute<DescriptionAttribute>(inherit);
            return desc?.Description ?? member.GetDisplayAttribute()?.Description ?? member.Name;
        }

        /// <summary>
        ///  获取类型元数据的Description特性描述信息
        /// </summary>
        /// <param name="object">类型元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DescriptionAttribute"/> 特性描述信息，如不存在则返回 null</returns>
        public static string GetDescription(this object @object, bool inherit = false)
        {
            Check.NotNull(@object, nameof(@object));
            return @object.GetType().GetDescription(inherit);
        }

        /// <summary>
        ///  获取成员元数据的 <see cref="DescriptionAttribute"/> 特性描述信息
        /// </summary>
        /// <param name="object">成员元数据对象</param>
        /// <param name="propertyExpression">属性</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DescriptionAttribute"/> 特性描述信息，如不存在则返回 null</returns>
        public static string GetDescription<TModel, TProperty>(this TModel @object, Expression<Func<TModel, TProperty>> propertyExpression, bool inherit = false)
        {
            Check.NotNull(@object, nameof(@object));
            Check.NotNull(propertyExpression, nameof(propertyExpression));
            MemberInfo member = (((dynamic)propertyExpression).Body).Member;
            return member.GetDescription(inherit);
        }


        /// <summary>
        ///  获取成员元数据的 <see cref="DisplayNameAttribute"/> 特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DisplayNameAttribute"/> 特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDisplayName(this MemberInfo member, bool inherit = false)
        {
            var desc = member.GetAttribute<DisplayNameAttribute>(inherit);
            return desc?.DisplayName ?? member.GetDisplayAttribute()?.Name ?? member.Name;
        }



        /// <summary>
        ///  获取类型元数据的 <see cref="DisplayNameAttribute"/> 特性描述信息
        /// </summary>
        /// <param name="object">类型元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DisplayNameAttribute"/> 特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDisplayName(this object @object, bool inherit = false)
        {
            Check.NotNull(@object, nameof(@object));
            return @object.GetType().GetDisplayName(inherit);
        }

        /// <summary>
        ///  获取成员元数据的 <see cref="DisplayNameAttribute.DisplayName"/> 特性描述信息
        /// </summary>
        /// <param name="object">成员元数据对象</param>
        /// <param name="propertyExpression">属性</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回 <see cref="DisplayNameAttribute"/> 特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDisplayName<TModel, TProperty>(this TModel @object, Expression<Func<TModel, TProperty>> propertyExpression, bool inherit = false)
        {
            MemberInfo member = ((dynamic)propertyExpression.Body).Member;
            return member.GetDisplayName(inherit);
        }

        /// <summary>
        /// Retrieves a <see cref="DisplayAttribute"/> of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <param name="member"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A <see cref="DisplayAttribute"/> that matches attributeType, or null if no such attribute is found.</returns>
        private static DisplayAttribute GetDisplayAttribute(this MemberInfo member, bool inherit = false)
        {
            return member.GetAttribute<DisplayAttribute>(inherit);
        }

        /// <summary>
        /// Retrieves a custom attribute of a specified objecct's type that is applied to a specified member, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="object"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A custom attribute that matches attributeType, or null if no such attribute is found.</returns>
        public static TAttribute GetAttribute<TAttribute>(this object @object, bool inherit = false) where TAttribute : System.Attribute
        {
            return @object.GetAttributes<TAttribute>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a collection of custom attributes of a specified object's type that are applied to a specified member, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="object"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A collection of the custom attributes that are applied to element and that match T, or an empty collection if no such attributes exist.</returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this object @object, bool inherit = false) where TAttribute : System.Attribute
        {
            Check.NotNull(@object,nameof(@object));
            return @object.GetType().GetAttributes<TAttribute>(inherit);
        }


        /// <summary>
        /// Checks if a custom attribute of the specified type applied to the specified member exists, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="memberInfo"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A custom attribute that matches attributeType, or null if no such attribute is found.</returns>
        public static bool AttributeExists<TAttribute>(this MemberInfo memberInfo, bool inherit = false) where TAttribute : System.Attribute
        {
            return memberInfo.GetAttribute<TAttribute>(inherit) != null;
        }

        /// <summary>
        /// Checks if a custom attribute of the specified object's type applied to the specified member exists, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="object"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A custom attribute that matches attributeType, or null if no such attribute is found.</returns>
        public static bool AttributeExists<TAttribute>(this object @object, bool inherit = false) where TAttribute : System.Attribute
        {
            Check.NotNull(@object, nameof(@object));
            return @object.GetType().AttributeExists<TAttribute>(inherit);
        }

        /// <summary>
        /// Retrieves a custom attribute of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="memberInfo"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A custom attribute that matches attributeType, or null if no such attribute is found.</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit = false) where TAttribute : System.Attribute
        {
            return memberInfo.GetAttributes<TAttribute>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a collection of custom attributes of a specified type that are applied to a specified member, and optionally inspects the ancestors of that member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to search for.</typeparam>
        /// <param name="memberInfo"> The member to inspect.</param>
        /// <param name="inherit">true to inspect the ancestors of element; otherwise, false.</param>
        /// <returns>A collection of the custom attributes that are applied to element and that match T, or an empty collection if no such attributes exist.</returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo memberInfo, bool inherit = false) where TAttribute : System.Attribute
        {
            return memberInfo.GetCustomAttributes<TAttribute>(inherit);
        }


    }
}
