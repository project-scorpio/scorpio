using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scorpio.Application.Dtos
{
    internal sealed class PagedResult<T> : ReadOnlyCollection<T>, IPagedResult<T>
    {
        public PagedResult(IList<T> source):base(source)
        {
        }
        public long TotalCount { get; set; }
    }
}
