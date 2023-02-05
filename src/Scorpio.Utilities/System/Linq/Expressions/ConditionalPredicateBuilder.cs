namespace System.Linq.Expressions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public partial class PredicateBuilder
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> OrElse<T, TResult>(
            this Expression<Func<T, TResult>> left,
            Expression<Func<T, TResult>> right) => BinaryCombine(left, right, (lt,rt)=>Expression.OrElse(lt,rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> OrElse<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> left,
            Expression<Func<T1, T2, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.OrElse(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> OrElse<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> left,
            Expression<Func<T1, T2, T3, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.OrElse(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> OrElse<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> left,
            Expression<Func<T1, T2, T3, T4, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.OrElse(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> OrElse<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> left,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.OrElse(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> AndAlso<T, TResult>(
            this Expression<Func<T, TResult>> left,
            Expression<Func<T, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.AndAlso(lt, rt));


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, TResult>> AndAlso<T1, T2, TResult>(
            this Expression<Func<T1, T2, TResult>> left,
                 Expression<Func<T1, T2, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.AndAlso(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, TResult>> AndAlso<T1, T2, T3, TResult>(
            this Expression<Func<T1, T2, T3, TResult>> left,
            Expression<Func<T1, T2, T3, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.AndAlso(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, TResult>> AndAlso<T1, T2, T3, T4, TResult>(
            this Expression<Func<T1, T2, T3, T4, TResult>> left,
            Expression<Func<T1, T2, T3, T4, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.AndAlso(lt, rt));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, T5, TResult>> AndAlso<T1, T2, T3, T4, T5, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, TResult>> left,
            Expression<Func<T1, T2, T3, T4, T5, TResult>> right) => BinaryCombine(left, right, (lt, rt) => Expression.AndAlso(lt, rt));

    }
}
