
namespace System.Linq.Expressions
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IExpressionTranslation<TSource, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource"></typeparam>
        /// <returns></returns>
        Expression<Func<TTranslatedSource, TResult>> To<TTranslatedSource>();


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        Expression<Func<TTranslatedSource, TResult>> To<TTranslatedSource>(Action<TranslatePathMapper<Func<TSource, TResult>>> action);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IExpressionTranslation<T1, T2, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> To<TTranslatedSource1, TTranslatedSource2>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TResult>> To<TTranslatedSource1, TTranslatedSource2>(Action<TranslatePathMapper<Func<T1, T2, TResult>>> action);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IExpressionTranslation<T1, T2, T3, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <typeparam name="TTranslatedSource3"></typeparam>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <typeparam name="TTranslatedSource3"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3>(Action<TranslatePathMapper<Func<T1, T2, T3, TResult>>> action);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IExpressionTranslation<T1, T2, T3, T4, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <typeparam name="TTranslatedSource3"></typeparam>
        /// <typeparam name="TTranslatedSource4"></typeparam>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTranslatedSource1"></typeparam>
        /// <typeparam name="TTranslatedSource2"></typeparam>
        /// <typeparam name="TTranslatedSource3"></typeparam>
        /// <typeparam name="TTranslatedSource4"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        Expression<Func<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4, TResult>> To<TTranslatedSource1, TTranslatedSource2, TTranslatedSource3, TTranslatedSource4>(Action<TranslatePathMapper<Func<T1, T2, T3, T4, TResult>>> action);

    }

}