using System;
using System.Collections.Generic;
using System.Linq.Async;
using System.Text;

using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Scorpio.EntityFrameworkCore
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    internal class QueryProvider : EntityQueryProvider, System.Linq.Async.IAsyncQueryProvider
    {
        public QueryProvider( IQueryCompiler queryCompiler) : base(queryCompiler)
        {
        }
    }
}
