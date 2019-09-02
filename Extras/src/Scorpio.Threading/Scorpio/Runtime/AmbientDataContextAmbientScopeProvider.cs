using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Scorpio.Runtime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Runtime
{
    internal class AmbientDataContextAmbientScopeProvider<T> : IAmbientScopeProvider<T>
    {

        private static readonly ConcurrentDictionary<string, ScopeItem> _scopeDictionary = new ConcurrentDictionary<string, ScopeItem>();

        private readonly IAmbientDataContext _dataContext;

        public AmbientDataContextAmbientScopeProvider(IAmbientDataContext dataContext)
        {
            Check.NotNull(dataContext, nameof(dataContext));

            _dataContext = dataContext;

        }

        public T GetValue(string contextKey)
        {
            var item = GetCurrentItem(contextKey);
            if (item == null)
            {
                return default;
            }

            return item.Value;
        }

        public IDisposable BeginScope(string contextKey, T value)
        {
            var item = new ScopeItem(value, GetCurrentItem(contextKey));

            if (!_scopeDictionary.TryAdd(item.Id, item))
            {
                throw new ScorpioException("Can not add item! ScopeDictionary.TryAdd returns false!");
            }

            _dataContext.SetData(contextKey, item.Id);

            return new DisposeAction(() =>
            {
                _scopeDictionary.TryRemove(item.Id, out item);

                if (item.Outer == null)
                {
                    _dataContext.SetData(contextKey, null);
                    return;
                }

                _dataContext.SetData(contextKey, item.Outer.Id);
            });
        }

        private ScopeItem GetCurrentItem(string contextKey)
        {
            return _dataContext.GetData(contextKey) is string objKey ? _scopeDictionary.GetOrDefault(objKey) : null;
        }

        private class ScopeItem
        {
            public string Id { get; }

            public ScopeItem Outer { get; }

            public T Value { get; }

            public ScopeItem(T value, ScopeItem outer = null)
            {
                Id = Guid.NewGuid().ToString();

                Value = value;
                Outer = outer;
            }
        }
    }
}
