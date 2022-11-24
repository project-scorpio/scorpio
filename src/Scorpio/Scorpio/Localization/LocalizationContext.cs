using System;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

using Scorpio.DependencyInjection;

namespace Scorpio.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizationContext : IServiceProviderAccessor
    {
        private static readonly AsyncLocal<LocalizationContext> _currnetContext = new AsyncLocal<LocalizationContext>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IDisposable Use(LocalizationContext context)
        {
            var current = _currnetContext.Value;
            _currnetContext.Value = context;
            return new DisposeAction(() => _currnetContext.Value = current);
        }

        /// <summary>
        /// 
        /// </summary>
        public static LocalizationContext Current=>_currnetContext.Value;
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public IStringLocalizerFactory LocalizerFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LocalizationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = Check.NotNull(serviceProvider, nameof(serviceProvider));
            LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
        }

    }
}