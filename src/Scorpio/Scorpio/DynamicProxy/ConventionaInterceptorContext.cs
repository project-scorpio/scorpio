using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Configuration;

namespace Scorpio.DynamicProxy
{
    internal class ConventionaInterceptorContext : IConventionaInterceptorContext
    {
        private readonly IDictionary<Type, TypeInterceptorMap> _maps;
        public ConventionaInterceptorContext(IDictionary<Type, TypeInterceptorMap> maps)
        {
            _maps = maps;
        }
        public void Add(Type serviceType, Type interceptor)
        {
            var map = _maps.GetOrAdd(interceptor, t => new TypeInterceptorMap(t));
            map.AddService(serviceType);
        }
    }
}
