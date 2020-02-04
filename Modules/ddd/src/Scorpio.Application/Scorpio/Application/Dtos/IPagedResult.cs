using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to return a page of items to clients.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="IListResult{T}"/> list</typeparam>
    public interface IPagedResult<out T>:IListResult<T>,ITotalCount
    {
    }
}
