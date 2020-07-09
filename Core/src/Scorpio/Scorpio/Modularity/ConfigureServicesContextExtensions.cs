using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.Conventional;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigureServicesContextExtensions
    {
        /// <summary>
        /// 向 <see cref="IServiceCollection"/> 集合添加 <see cref="IConventionalRegistrar"/> 对象实例，用于为 <see cref="RegisterAssemblyByConvention(ConfigureServicesContext, Assembly)"/> 方法提供通用注册者。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static ConfigureServicesContext AddConventionalRegistrar(this ConfigureServicesContext context, IConventionalRegistrar registrar)
        {
            context.Services.AddConventionalRegistrar(registrar);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConfigureServicesContext AddConventionalRegistrar<T>(this ConfigureServicesContext context)
          where T : IConventionalRegistrar
        {
            return context.AddConventionalRegistrar(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static ConfigureServicesContext RegisterAssemblyByConvention(this ConfigureServicesContext context, Assembly assembly)
        {
            context.Services.RegisterAssemblyByConvention(assembly);
            return context;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConfigureServicesContext RegisterAssemblyByConvention(this ConfigureServicesContext context)
        {
            var assembly = Assembly.GetCallingAssembly();
            return RegisterAssemblyByConvention(context, assembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConfigureServicesContext RegisterAssemblyByConventionOfType<T>(this ConfigureServicesContext context)
        {
            return context.RegisterAssemblyByConvention(typeof(T).GetTypeInfo().Assembly);
        }
    }
}
