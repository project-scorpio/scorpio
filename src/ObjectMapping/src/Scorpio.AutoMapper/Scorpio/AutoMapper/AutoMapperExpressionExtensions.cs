using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Scorpio.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperExpressionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mappingExpression"></param>
        /// <param name="destinationMember"></param>
        /// <returns></returns>
        public static IMappingExpression<TDestination, TMember> Ignore<TDestination, TMember, TResult>(this IMappingExpression<TDestination, TMember> mappingExpression, Expression<Func<TMember, TResult>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, opts => opts.Ignore());
        }
        

    }
}
