using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    internal class ScorpioDbContextOptionsBuilder : IScorpioDbContextOptionsBuilder
    {
        public IServiceCollection Services { get; }

        public ScorpioDbContextOptionsBuilder(IServiceCollection services)
        {
            Services = services;
        }

    }

    internal class ScorpioDbContextOptionsBuilder<TDbContext> : ScorpioDbContextOptionsBuilder, IScorpioDbContextOptionsBuilder<TDbContext>
    where TDbContext : ScorpioDbContext<TDbContext>
    {

        public List<Action<DbContextOptionsBuilder<TDbContext>>> OptionsActions { get; }

        public ScorpioDbContextOptionsBuilder(IServiceCollection services):base(services)
        {
            OptionsActions = new List<Action<DbContextOptionsBuilder<TDbContext>>>();
        }

    }

}
