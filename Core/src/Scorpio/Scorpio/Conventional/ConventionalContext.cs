using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Conventional
{
    internal class ConventionalContext:IConventionalContext
    {
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();
        private readonly IEnumerable<Type> _types;

        public IServiceCollection  Services { get;  }


        public Expression<Func<Type,bool>> TypePredicate { get; private set; }

        public IEnumerable<Type> Types => _types.Where(TypePredicate.Compile());

        public ConventionalContext(IServiceCollection services, IEnumerable<Type> types)
        {
            Services = services;
            _types = types;
        }

        public void AddPredicateExpression(Expression<Func<Type,bool>> expression)
        {
            TypePredicate = TypePredicate == null ? expression : TypePredicate.AndAlso(expression);
        }

        public void AddPredicate(Predicate<Type> predicate)
        {
            AddPredicateExpression(t => predicate(t));
        }

        public T GetItem<T>(string key)
        {
            if(!_items.TryGetValue(key, out var value))
            {
                return default;
            }
            return (T)value;
        }

        public void SetItem<T>(string key,T item)
        {
            _items[key] = item;
        }
    }

    internal class ConventionalContext<TAction> : ConventionalContext, IConventionalContext<TAction>
    {
        public ConventionalContext(IServiceCollection services, IEnumerable<Type> types) : base(services, types)
        {
        }
    }
}
