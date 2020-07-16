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
            if (!(path.Body is MemberExpression member))
                throw new NotSupportedException("Only member expressions are supported yet.");
            var bind = Expression.Bind(member.Member, _predicate.Body);

            return Expression.Lambda<Func<TTranslatedSource, TTranslatedSource>>(
              Expression.MemberInit(Expression.New(typeof(TTranslatedSource)), bind), t);
        }
    }

}
