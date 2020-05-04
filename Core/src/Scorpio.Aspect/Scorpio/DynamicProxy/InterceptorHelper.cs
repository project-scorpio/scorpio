using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Configuration;
using System.Linq;
using System.Reflection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Scorpio.DynamicProxy
{
    /// <summary>
    /// 
    /// </summary>
    internal static class InterceptorHelper
    {
        public static List<IConventionaInterceptorRegistrar> Registrars { get; } = new List<IConventionaInterceptorRegistrar>();
        /// <summary>
        /// 添加注册
        /// </summary>
        /// <param name="registrar"></param>
        public static void AddConventionalRegistrar(IConventionaInterceptorRegistrar registrar)
        {
            Registrars.Add(registrar);
        }

        public static void RegisterConventionalInterceptor(IServiceCollection services)
        {
            var maps = new Dictionary<Type, TypeInterceptorMap>();
            var context = new ConventionaInterceptorContext(maps);
            Registrars.ForEach(r => r.Register(context));
            services.ConfigureDynamicProxy(c =>
            {
                maps.Values.ForEach(m =>
                {
                    services.TryAddTransient(m.InterceptorType);
                    c.Interceptors.AddServiced(m.InterceptorType, GetPredicate(m.ServiceTypes));
                });
            });

        }

        public static AspectPredicate GetPredicate(IList<Type> types)
        {
            return m => !m.AttributeExists<NonAspectAttribute>() && types.Any(t => m.DeclaringType.IsAssignableTo(t));
        }

    }
}
