namespace System.Linq.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public static class TranslationBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IExpressionTranslation<TSource, TResult> Translate<TSource, TResult>(this Expression<Func<TSource, TResult>> predicate) => new ExpressionTranslation<TSource, TResult>(predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IExpressionTranslation<T1, T2, TResult> Translate<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> predicate) => new ExpressionTranslation<T1, T2, TResult>(predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IExpressionTranslation<T1, T2, T3, TResult> Translate<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> predicate) => new ExpressionTranslation<T1, T2, T3, TResult>(predicate);

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
        public static IExpressionTranslation<T1, T2, T3, T4, TResult> Translate<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> predicate) => new ExpressionTranslation<T1, T2, T3, T4, TResult>(predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static MemberInitTranslation<TSource> Translate<TSource>(this Expression<Func<TSource, TSource>> predicate) => new MemberInitTranslation<TSource>(predicate);

    }
}
