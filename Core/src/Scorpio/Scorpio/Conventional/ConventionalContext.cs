using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Conventional
{
    internal class ConventionalContext:IConventionalContext
    {
        public IServiceCollection  Services { get;  }

        public Expression<Predicate<Type>> TypePredicate { get; private set; }

        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

        public ConventionalContext(IServiceCollection services)
        {
            Services = services;
        }

        public void AddPredicateExpression(Expression<Predicate<Type>> expression)
        {
            TypePredicate = TypePredicate == null ? expression : Expression.Lambda<Predicate<Type>>(Expression.AndAlso(TypePredicate, expression));
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
}
