using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
   public interface IDbContextProvider<TDbContext>
        where TDbContext:DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TDbContext GetDbContext();

    }
}
