namespace System.Linq.Expressions
{
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
            var binds = init.Bindings
                .OfType<MemberAssignment>()
                .Select(b => (b.Member.Name, b.Expression))
                .Select(b => Expression.Bind(type.GetProperty(b.Name), GetExpression(t, b.Expression, b.Name)))
                .ToList();
            return Expression.Lambda<Func<TTranslatedSource, TTranslatedSource>>(
                 Expression.MemberInit(Expression.New(typeof(TTranslatedSource)), binds), t);

            static Expression GetExpression(Expression parameter,
                                            Expression expression,
                                            string propertyName)
            {
                return expression.NodeType switch
                {
                    ExpressionType.MemberAccess => Expression.Property(parameter, propertyName),
                    _ => expression
                };
            }
        }

    }

}
