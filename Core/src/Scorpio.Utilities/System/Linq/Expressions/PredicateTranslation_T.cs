using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    internal sealed class PredicateTranslation<TSource, TResult> : PredicateTranslation<Func<TSource, TResult>>, IPredicateTranslation<TSource, TResult>
    {
        public PredicateTranslation(Expression<Func<TSource, TResult>> predicate) : base(predicate)
        {
        }

        public Expression<Func<TTranslatedSource, TResult>> To<TTranslatedSource>()
        {
            return To<Func<TTranslatedSource, TResult>>(typeof(TTranslatedSource));
        }

        public Expression<Func<TTranslatedSource,TResult>> To<TTranslatedSource>(Action<TranslatePathMapper<Func<TSource,TResult>>> action)
        {
            return base.To<Func<TTranslatedSource, TResult>>(action);
        }
    }

    internal sealed class PredicateTranslation<T1, T2, TResult> : PredicateTranslation<Func<T1, T2, TResult>>, IPredicateTranslation<T1, T2, TResult>
    {
        public PredicateTranslation(Expression<Func<T1, T2, TResult>> predicate) : base(predicate)
        {
        }

        public Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> To<TTranslatedSource1, TTranslatedSource2>()
        {
            return To<Func<TTranslatedSource1, TTranslatedSource2, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2));
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> To<TTranslatedSource1, TTranslatedSource2>(Action<TranslatePathMapper<Func<T1, T2, TResult>>> action)
        {
            return base.To<Func<TTranslatedSource1, TTranslatedSource2, TResult>>(action);
        }
    }

    internal sealed class PredicateTranslation<T1, T2, T3, TResult> : PredicateTranslation<Func<T1, T2, T3, TResult>>, IPredicateTranslation<T1, T2, T3, TResult>
    {
        public PredicateTranslation(Expression<Func<T1, T2, T3, TResult>> predicate) : base(predicate)
        {
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>()
        {
            return To<Func<TTranslatedSource1, TTranslatedSource2,TTranslatedSource3, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2), typeof(TTranslatedSource3));
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>(Action<TranslatePathMapper<Func<T1, T2, T3, TResult>>> action)
        {
            return base.To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>>(action);
        }
    }

    internal sealed class PredicateTranslation<T1, T2, T3,T4, TResult> : PredicateTranslation<Func<T1, T2, T3,T4, TResult>>, IPredicateTranslation<T1, T2, T3,T4, TResult>
    {
        public PredicateTranslation(Expression<Func<T1, T2, T3,T4, TResult>> predicate) : base(predicate)
        {
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>()
        {
            return To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2), typeof(TTranslatedSource3));
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>()
        {
            return To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3,TTranslatedSource4, TResult>>(typeof(TTranslatedSource1), typeof(TTranslatedSource2), typeof(TTranslatedSource3),typeof(TTranslatedSource4));
        }

        public new Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>(Action<TranslatePathMapper<Func<T1, T2, T3, T4, TResult>>> action)
        {
            return base.To<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>>(action);
        }
    }

}
