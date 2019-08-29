using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Collections.Immutable;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services, IConventionalRegistrar registrar)
        {
            GetOrCreateRegistrarList().Add(registrar);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar<T>(this IServiceCollection services)
          where T : IConventionalRegistrar
        {
            return services.AddConventionalRegistrar(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConvention(this IServiceCollection services, Assembly assembly)
        {
            var context = new ConventionalRegistrationContext(assembly, services);
            GetOrCreateRegistrarList().ToImmutableList().ForEach(registrar => registrar.Register(context));
            return services;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConvention(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            return RegisterAssemblyByConvention(services, assembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConventionOfType<T>(this IServiceCollection services)
        {
            return services.RegisterAssemblyByConvention(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ConventionalRegistrarList GetOrCreateRegistrarList()
        {
            return ConventionalRegistrarList.Registrars;
        }
    }
}
