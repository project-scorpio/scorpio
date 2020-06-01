using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    partial class PredicateBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> ExclusiveOr<T, TResult>(
            this Expression<Func<T, TResult>> left,
            Expression<Func<T, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.ExclusiveOr(lft, rit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> ExclusiveOr<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> left,
            Expression<Func<T1, T2, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.ExclusiveOr(lft, rit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> ExclusiveOr<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> left,
            Expression<Func<T1, T2, T3, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.ExclusiveOr(lft, rit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> ExclusiveOr<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> left,
            Expression<Func<T1, T2, T3, T4, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.ExclusiveOr(lft, rit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> ExclusiveOr<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> left,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.ExclusiveOr(lft, rit));
        }

    }
}
