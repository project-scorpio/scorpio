using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Linq.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PredicateBuilder
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
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> And<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.And(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> And<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> expr1, Expression<Func<T1, T2, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.And(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> And<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expr1,
            Expression<Func<T1, T2, T3, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.And(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> And<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.And(l, r));
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
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> And<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.And(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> AndAlso<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.AndAlso(l, r));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> AndAlso<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> expr1,
                 Expression<Func<T1, T2, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.AndAlso(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> AndAlso<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expr1,
            Expression<Func<T1, T2, T3, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.AndAlso(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> AndAlso<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.AndAlso(l, r));
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
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> AndAlso<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.AndAlso(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> Equal<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.Equal(l, r));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> Equal<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> expr1,
                 Expression<Func<T1, T2, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.Equal(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> Equal<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> expr1,
            Expression<Func<T1, T2, T3, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.Equal(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> Equal<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.Equal(l, r));
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
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> Equal<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> expr1,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> expr2)
        {
            return Combine(expr1, expr2, (l, r) => Expression.Equal(l, r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <param name="combineFunc"></param>
        /// <returns></returns>
        public static Expression<TDelegate> Combine<TDelegate>(this Expression<TDelegate> expr1, Expression<TDelegate> expr2, Func<Expression, Expression, BinaryExpression> combineFunc)
        {
            var (left, right, parameters) = MergeExpressionAndParameters(expr1, expr2);
            return Expression.Lambda<TDelegate>(combineFunc(left, right), parameters);

        }

        private static (Expression left, Expression right, ParameterExpression[] parameters) MergeExpressionAndParameters(LambdaExpression left, LambdaExpression right)
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IPredicateTranslation<TSource, TResult> Translate<TSource, TResult>(this Expression<Func<TSource, TResult>> predicate)
        {
            return new PredicateTranslation<TSource, TResult>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IPredicateTranslation<T1, T2, TResult> Translate<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> predicate)
        {
            return new PredicateTranslation<T1, T2, TResult>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IPredicateTranslation<T1, T2, T3, TResult> Translate<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> predicate)
        {
            return new PredicateTranslation<T1, T2, T3, TResult>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IPredicateTranslation<T1, T2, T3, T4, TResult> Translate<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> predicate)
        {
            return new PredicateTranslation<T1, T2, T3, T4, TResult>(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static MemberInitTranslation<TSource> Translate<TSource>(this Expression<Func<TSource, TSource>> predicate)
        {
            return new MemberInitTranslation<TSource>(predicate);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        public sealed class MemberInitTranslation<TSource>
        {
            private readonly Expression<Func<TSource, TSource>> _predicate;

            internal MemberInitTranslation(Expression<Func<TSource, TSource>> predicate)
            {
                this._predicate = predicate;
            }

            /// <summary>
            /// Translates a given predicate for a given subtype.
            /// </summary>
            /// <typeparam name="TTranslatedSource">The type of the translated predicate's parameter.</typeparam>
            /// <returns>A translated predicate expression.</returns>
            public Expression<Func<TTranslatedSource, TTranslatedSource>> To<TTranslatedSource>()
            {
                var type = typeof(TTranslatedSource);
                var s = _predicate.Parameters[0];
                var t = Expression.Parameter(type, s.Name);
                var init = _predicate.Body as MemberInitExpression;

                var binds = init.Bindings.OfType<MemberAssignment>().Select(b => Expression.Bind(type.GetProperty(b.Member.Name), b.Expression));
                return Expression.Lambda<Func<TTranslatedSource, TTranslatedSource>>(
                     Expression.MemberInit(Expression.New(typeof(TTranslatedSource)), binds), t);
            }

            /// <summary>
            /// Translates a given predicate for a given related type.
            /// </summary>
            /// <typeparam name="TTranslatedSource">The type of the translated predicate's parameter.</typeparam>
            /// <param name="path">The path from the desired type to the given type.</param>
            /// <returns>A translated predicate expression.</returns>
            public Expression<Func<TTranslatedSource, TTranslatedSource>> To<TTranslatedSource>(Expression<Func<TTranslatedSource, TSource>> path)
            {
                if (path == null)
                    throw new ArgumentNullException(nameof(path));

                var t = path.Parameters[0];
                var member = path.Body as MemberExpression;
                if (member == null)
                    throw new NotSupportedException("Only member expressions are supported yet.");

                var bind = Expression.Bind(member.Member, _predicate.Body); ;

                return Expression.Lambda<Func<TTranslatedSource, TTranslatedSource>>(
                  Expression.MemberInit(Expression.New(typeof(TTranslatedSource)), bind), t);
            }
        }

    }
}
