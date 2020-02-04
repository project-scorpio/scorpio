using Scorpio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class MappingExpressionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="destinationMember"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> MapFromExtraProperty<TSource, TDestination, TMember>(
            this IMappingExpression<TSource, TDestination> mapping,
            Expression<Func<TDestination, TMember>> destinationMember)
            where TSource : IHasExtraProperties
        {
            var member = (destinationMember.Body as MemberExpression).Member;
            var name = member.Name;
            mapping.ForMember(destinationMember, o => o.MapFrom(u => (TMember)u.ExtraProperties[name]));
            return mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> MapExtraProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mapping, 
            params Expression<Func<TSource, object>>[] properties)
            where TDestination : IHasExtraProperties
        {
            var expression = GenerateInitExpression(properties);
            mapping.ForMember(d => d.ExtraProperties, o => o.MapFrom(expression));
            return mapping;
        }

        private static Expression<Func<TSource, Dictionary<string, object>>> GenerateInitExpression<TSource>(
            Expression<Func<TSource, object>>[] expressions)
        {
            var type = typeof(Dictionary<string, object>);
            var method = type.GetMethod("Add");
            var parameter = Expression.Parameter(typeof(TSource), "s");
            var initexpression = Expression.ListInit(Expression.New(type), GenerateElements(method, parameter, expressions));
            var expression = Expression.Lambda<Func<TSource, Dictionary<string, object>>>(initexpression, parameter);
            return expression;
        }

        private static IEnumerable<ElementInit> GenerateElements<TSource>(
            System.Reflection.MethodInfo method, 
            ParameterExpression parameter, 
            Expression<Func<TSource, object>>[] expressions)
        {
            return expressions
                .Select(p => p.Body as MemberExpression)
                .Select(p =>
                    Expression.ElementInit(method, Expression.Constant(p.Member.Name), Expression.Convert(Expression.Property(parameter, p.Member.Name), typeof(object))));
        }
    }
}
