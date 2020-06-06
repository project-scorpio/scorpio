using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Linq.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class PredicateBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> Equal<T, TResult>(
            this Expression<Func<T, TResult>> left, 
            Expression<Func<T, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.Equal(lft, rit));
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
        public static Expression<Func<T1, T2, TResult>> Equal<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> left,
                 Expression<Func<T1, T2, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.Equal(lft, rit));
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
        public static Expression<Func<T1, T2, T3, TResult>> Equal<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> left,
            Expression<Func<T1, T2, T3, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.Equal(lft, rit));
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
        public static Expression<Func<T1, T2, T3, T4, TResult>> Equal<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> left,
            Expression<Func<T1, T2, T3, T4, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.Equal(lft, rit));
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
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> Equal<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> left,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> right)
        {
            return Combine(left, right, (lft, rit) => Expression.Equal(lft, rit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="combineFunc"></param>
        /// <returns></returns>
        public static Expression<TDelegate> Combine<TDelegate>(
            this Expression<TDelegate> left, 
            Expression<TDelegate> right, 
            Func<Expression, Expression, BinaryExpression> combineFunc)
        {
            var (lft, rit, parameters) = MergeExpressionAndParameters(left, right);
            return Expression.Lambda<TDelegate>(combineFunc(lft, rit), parameters);

        }

        private static (Expression left, Expression right, ParameterExpression[] parameters) MergeExpressionAndParameters(
            LambdaExpression left, 
            LambdaExpression right)
        {
            var parameters = new ParameterExpression[left.Parameters.Count];
            var leftExpression = left.Body;
            var rightExpression = right.Body;
            for (int i = 0; i < left.Parameters.Count; i++)
            {
                parameters[i] = Expression.Parameter(left.Parameters[i].Type);
                var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[i], parameters[i]);
                leftExpression = leftVisitor.Visit(leftExpression);

                var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[i], parameters[i]);
                rightExpression = rightVisitor.Visit(rightExpression);
            }
            return (leftExpression, rightExpression, parameters);
        }

    }
}
