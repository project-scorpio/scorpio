using System;

using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EntityFrameworkCore
{
    internal class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultDbContextProvider(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public TDbContext GetDbContext() => _serviceProvider.GetRequiredService<TDbContext>();
    }
}