using System.Collections.Generic;

namespace System.Linq.Expressions
{

    internal abstract class ExpressionTranslation<TDelegate>
    {
        private readonly Expression<TDelegate> _predicate;

        protected ExpressionTranslation(Expression<TDelegate> predicate)
        {
            this._predicate = predicate;
        }

        protected Expression<TTranslatedDelegate> To<TTranslatedDelegate>(params Type[] parameterTypes)
        {
            return To<TTranslatedDelegate>((IEnumerable<Type>)parameterTypes);
        }


        protected Expression<TTranslatedDelegate> To<TTranslatedDelegate>(IEnumerable<Type> parameterTypes)
        {
            var (expression, parameters) = MergeExpressionAndParameters(parameterTypes);
            return Expression.Lambda<TTranslatedDelegate>(expression, parameters);
        }

        private (Expression expression, ParameterExpression[] parameters) MergeExpressionAndParameters(IEnumerable<Type> parameterTypes)
        {
            var parameters = new ParameterExpression[_predicate.Parameters.Count];
            _predicate.Parameters.CopyTo(parameters, 0);
            var expression = _predicate.Body;
            var types = parameterTypes.GetEnumerator();
            for (var i = 0; i < _predicate.Parameters.Count; i++)
            {
                types.MoveNext();
                var s = _predicate.Parameters[i];
                parameters[i] = Expression.Parameter(types.Current, s.Name);
                var binder = new ReplaceExpressionVisitor(s, parameters[i]);
                expression = binder.Visit(expression);
            }
            return (expression, parameters);
        }

        public Expression<TTranslatedDelegate> To<TTranslatedDelegate>(Action<TranslatePathMapper<TDelegate>> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            var mapper = new TranslatePathMapper<TDelegate>(_predicate);
            action(mapper);
            var (expression, parameters) = mapper.MergeExpressionAndParameters<TTranslatedDelegate>();

            return Expression.Lambda<TTranslatedDelegate>(expression, parameters);
        }
    }

}
