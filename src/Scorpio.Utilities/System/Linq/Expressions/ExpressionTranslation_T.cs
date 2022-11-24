namespace System.Linq.Expressions
{
    internal sealed class ExpressionTranslation<TSource, TResult> : ExpressionTranslation<Func<TSource, TResult>>, IExpressionTranslation<TSource, TResult>
    {
        public ExpressionTranslation(Expression<Func<TSource, TResult>> predicate) : base(predicate)
        {
        }

        Expression<Func<TTranslatedSource, TResult>> IExpressionTranslation<TSource, TResult>.To<TTranslatedSource>() => To<Func<TTranslatedSource, TResult>>(typeof(TTranslatedSource));

        Expression<Func<TTranslatedSource, TResult>> IExpressionTranslation<TSource, TResult>.To<TTranslatedSource>(Action<TranslatePathMapper<Func<TSource, TResult>>> action) => base.To<Func<TTranslatedSource, TResult>>(action);
    }

    [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    internal sealed class ExpressionTranslation<T1, T2, TResult> : ExpressionTranslation<Func<T1, T2, TResult>>, IExpressionTranslation<T1, T2, TResult>
    {
        public ExpressionTranslation(Expression<Func<T1, T2, TResult>> predicate) : base(predicate)
        {
        }

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> IExpressionTranslation<T1, T2, TResult>.To<TTranslatedSource1, TTranslatedSource2>() => To<Func<TTranslatedSource1, TTranslatedSource2, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2));

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> IExpressionTranslation<T1, T2, TResult>.To<TTranslatedSource1, TTranslatedSource2>(Action<TranslatePathMapper<Func<T1, T2, TResult>>> action) => base.To<Func<TTranslatedSource1, TTranslatedSource2, TResult>>(action);
    }

    [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    internal sealed class ExpressionTranslation<T1, T2, T3, TResult> : ExpressionTranslation<Func<T1, T2, T3, TResult>>, IExpressionTranslation<T1, T2, T3, TResult>
    {
        public ExpressionTranslation(Expression<Func<T1, T2, T3, TResult>> predicate) : base(predicate)
        {
        }

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> IExpressionTranslation<T1, T2, T3, TResult>.To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>() => To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2), typeof(TTranslatedSource3));

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> IExpressionTranslation<T1, T2, T3, TResult>.To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>(Action<TranslatePathMapper<Func<T1, T2, T3, TResult>>> action) => base.To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>>(action);
    }

    [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    internal sealed class ExpressionTranslation<T1, T2, T3, T4, TResult> : ExpressionTranslation<Func<T1, T2, T3, T4, TResult>>, IExpressionTranslation<T1, T2, T3, T4, TResult>
    {
        public ExpressionTranslation(Expression<Func<T1, T2, T3, T4, TResult>> predicate) : base(predicate)
        {
        }

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> IExpressionTranslation<T1, T2, T3, T4, TResult>.To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>() => To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2), typeof(TTranslatedSource3), typeof(TTranslatedSource4));

        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> IExpressionTranslation<T1, T2, T3, T4, TResult>.To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>(Action<TranslatePathMapper<Func<T1, T2, T3, T4, TResult>>> action) => base.To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>>(action);
    }

}
