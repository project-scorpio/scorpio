using Microsoft.EntityFrameworkCore;

using Scorpio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioDbContextOptionsBuilder<TDbContext> : IScorpioDbContextOptionsBuilder
        where TDbContext : ScorpioDbContext<TDbContext>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioDbContextOptionsBuilder
    {

    }
}
