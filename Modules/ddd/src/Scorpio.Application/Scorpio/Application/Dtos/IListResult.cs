using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to return a list of items to clients.
    /// </summary>
    /// <typeparam name="T">Type of the items in the  list</typeparam>
    public interface IListResult<out T> : IReadOnlyCollection<T>
    {

    }
}
