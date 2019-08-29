using System;
using System.Collections.Generic;
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
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Equal<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static PredicateTranslation<TSource> Translate<TSource>(this Expression<Func<TSource,bool>> predicate)
        {
            return new PredicateTranslation<TSource>(predicate);
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

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }
                return base.VisitParameter(node);
            }

            protected override Expression VisitInvocation(InvocationExpression node)
            {
                if (node.Expression==_oldValue && _newValue is LambdaExpression lambda)
                {
                    var buiders = lambda.Parameters.Zip(node.Arguments, (p, a) => new ReplaceExpressionVisitor(p, a));
                    return buiders.Aggregate(lambda.Body, (e, b) => b.Visit(e));
                }
                return base.VisitInvocation(node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        public sealed class PredicateTranslation<TSource>
        {
            private readonly Expression<Func<TSource, bool>> _predicate;

            internal PredicateTranslation(Expression<Func<TSource,bool>> predicate)
            {
                this._predicate = predicate;
            }

            /// <summary>
            /// Translates a given predicate for a given subtype.
            /// </summary>
            /// <typeparam name="TTranslatedSource">The type of the translated predicate's parameter.</typeparam>
            /// <returns>A translated predicate expression.</returns>
            public Expression<Func<TTranslatedSource, bool>> To<TTranslatedSource>()
            {
                var s = _predicate.Parameters[0];
                var t = Expression.Parameter(typeof(TTranslatedSource), s.Name);

                var binder = new ReplaceExpressionVisitor(s, t);

                return Expression.Lambda<Func<TTranslatedSource, bool>>(
                    binder.Visit(_predicate.Body), t);
            }

            /// <summary>
            /// Translates a given predicate for a given related type.
            /// </summary>
            /// <typeparam name="TTranslatedSource">The type of the translated predicate's parameter.</typeparam>
            /// <param name="path">The path from the desired type to the given type.</param>
            /// <returns>A translated predicate expression.</returns>
            public Expression<Func<TTranslatedSource, bool>> To<TTranslatedSource>(Expression<Func<TTranslatedSource, TSource>> path)
            {
                if (path == null)
                    throw new ArgumentNullException(nameof(path));

                var s = _predicate.Parameters[0];
                var t = path.Parameters[0];

                var binder = new ReplaceExpressionVisitor(s, path.Body);

                return Expression.Lambda<Func<TTranslatedSource, bool>>(
                    binder.Visit(_predicate.Body), t);
            }

            /// <summary>
            /// Translates a given predicate for a given related type.
            /// </summary>
            /// <typeparam name="TTranslatedSource">The type of the translated predicate's parameter.</typeparam>
            /// <param name="translation">The translation from the desired type to the given type,
            /// using the initially given predicate to be injected into a new predicate.</param>
            /// <returns>A translated predicate expression.</returns>
            public Expression<Func<TTranslatedSource, bool>> To<TTranslatedSource>(Expression<Func<TTranslatedSource, Func<TSource, bool>, bool>> translation)
            {
                if (translation == null)
                    throw new ArgumentNullException(nameof(translation));

                var t = translation.Parameters[0];
                var s = translation.Parameters[1];

                var binder = new ReplaceExpressionVisitor(s, _predicate);

                return Expression.Lambda<Func<TTranslatedSource, bool>>(
                    binder.Visit(translation.Body), t);
            }
        }

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

             var binds=   init.Bindings.OfType<MemberAssignment>().Select(b => Expression.Bind(type.GetProperty(b.Member.Name), b.Expression));
                return Expression.Lambda<Func<TTranslatedSource, TTranslatedSource>>(
                     Expression.MemberInit(Expression.New(typeof(TTranslatedSource)),binds), t);
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
