using System.Linq.Expressions;
using System.Threading;

namespace System.Linq.Async
{
    /// <summary>
    ///     <para>
    ///         Defines method to execute queries asynchronously that are described by an IQueryable object.
    ///     </para>
    /// </summary>
    public interface IAsyncQueryProvider : IQueryProvider
    {
        /// <summary>
        ///     Executes the strongly-typed query represented by a specified expression tree asynchronously.
        /// </summary>
        TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default);
    }
}
