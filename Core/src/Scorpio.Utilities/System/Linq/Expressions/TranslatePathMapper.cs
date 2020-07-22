using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDelegate"></typeparam>
    public sealed class TranslatePathMapper<TDelegate>
    {
        private readonly Expression<TDelegate> _predicate;
        private readonly List<LambdaExpression> _expressions = new List<LambdaExpression>();

        internal TranslatePathMapper(Expression<TDelegate> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTranslatedSource"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public TranslatePathMapper<TDelegate> Map<TSource, TTranslatedSource>(Expression<Func<TTranslatedSource, TSource>> path)
        {
            _expressions.Add(path);
            return this;
        }

        internal (Expression expression, ParameterExpression[] parameters) MergeExpressionAndParameters<TTranslatedDelegate>()
        {
            var parameters = new ParameterExpression[_predicate.Parameters.Count];
            _predicate.Parameters.CopyTo(parameters, 0);
            var expression = _predicate.Body;
            var translatedParameters = (typeof(TTranslatedDelegate)).GenericTypeArguments;
            for (var i = 0; i < _predicate.Parameters.Count; i++)
            {
                var s = _predicate.Parameters[i];
                var path = _expressions.Find(e => e.ReturnType == s.Type && e.Type.GenericTypeArguments[0]== translatedParameters[i]);
                if (path == null)
                {
                    continue;
                }
                path = TranslatePathMapper.UpdateExpression(path, s.Name);
                parameters[i] = path.Parameters[0];
                var binder = new ReplaceExpressionVisitor(s, path.Body);
                expression = binder.Visit(expression);
            }
            return (expression, parameters);
        }

    }

    internal class TranslatePathMapper
    {
        private static readonly MethodInfo _method = typeof(TranslatePathMapper).GetMethod(nameof(Update), BindingFlags.NonPublic | BindingFlags.Static);

        public static LambdaExpression UpdateExpression(LambdaExpression expression, string name)
        {
            var method = _method.MakeGenericMethod(expression.Parameters[0].Type, expression.ReturnType);
            return method.Invoke(null, new object[] { expression, name }) as LambdaExpression;
        }
        private static LambdaExpression Update<TTranslatedSource, TSource>(LambdaExpression path, string name)
        {
            var expression = path as Expression<Func<TTranslatedSource, TSource>>;
            var para = Expression.Parameter(typeof(TTranslatedSource), name);
            var body = (expression.Body as UnaryExpression).Update(para);
            return expression.Update(body, new ParameterExpression[] { para });
        }
    }

}
