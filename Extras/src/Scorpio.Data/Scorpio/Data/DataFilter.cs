using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataFilter : IDataFilter, ISingletonDependency
    {
        private readonly ConcurrentDictionary<Type, object> _filters;

        private readonly IServiceProvider _serviceProvider;

        private static readonly MethodInfo _isEnabledMethodInfo = typeof(DataFilter).GetMethods().Single(m => m.Name == nameof(IsEnabled) && m.IsGenericMethodDefinition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DataFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _filters = new ConcurrentDictionary<Type, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public IDisposable Enable<TFilter>()
            where TFilter : class
        {
            return GetFilter<TFilter>().Enable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public IDisposable Disable<TFilter>()
            where TFilter : class
        {
            return GetFilter<TFilter>().Disable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public bool IsEnabled<TFilter>()
            where TFilter : class
        {
            return GetFilter<TFilter>().IsEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsEnabled(Type type)
        {
            return (bool)_isEnabledMethodInfo.MakeGenericMethod(type).Invoke(this,null);
        }

        private IDataFilter<TFilter> GetFilter<TFilter>()
            where TFilter : class
        {
            return _filters.GetOrAdd(
                typeof(TFilter),
                () => _serviceProvider.GetRequiredService<IDataFilter<TFilter>>()
            ) as IDataFilter<TFilter>;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    internal class DataFilter<TFilter> : IDataFilter<TFilter>
        where TFilter : class
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                EnsureInitialized();
                return _filter.Value.IsEnabled;
            }
        }

        private readonly DataFilterOptions _options;

        private readonly AsyncLocal<DataFilterState> _filter;

        public DataFilter()
        {
            _filter = new AsyncLocal<DataFilterState>();

        }

        public DataFilter(bool isEnabled):this()
        {
            EnsureInitialized();
            _filter.Value.IsEnabled = isEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DataFilter(IOptions<DataFilterOptions> options):this()
        {
            _options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDisposable Enable()
        {
            if (IsEnabled)
            {
                return NullDisposable.Instance;
            }

            _filter.Value.IsEnabled = true;

            return new DisposeAction(() => Disable());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDisposable Disable()
        {
            if (!IsEnabled)
            {
                return NullDisposable.Instance;
            }

            _filter.Value.IsEnabled = false;

            return new DisposeAction(() => Enable());
        }

        private void EnsureInitialized()
        {
            if (_filter.Value != null)
            {
                return;
            }

            _filter.Value = _options?.Descriptors?.GetOrDefault(typeof(TFilter))?.GetState() ?? new DataFilterState(true);
        }
    }
}
