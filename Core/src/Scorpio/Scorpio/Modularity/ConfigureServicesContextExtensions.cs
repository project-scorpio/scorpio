using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
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
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ConfigureServicesContext AddConventionalRegistrar<T>(this ConfigureServicesContext context)
            where T : IConventionalRegistrar
        {
            context.Services.AddConventionalRegistrar<T>();
            return context;
        }

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
        public static ConfigureServicesContext RegisterAssemblyByConvention(this ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static ConfigureServicesContext RegisterAssemblyByConvention(this ConfigureServicesContext context,Assembly assembly)
        {
            context.Services.RegisterAssemblyByConvention(assembly);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConfigureServicesContext RegisterAssemblyByConventionOfType<T>(this ConfigureServicesContext  context)
        {
            context.Services.RegisterAssemblyByConventionOfType<T>();
            return context;
        }

    }
}
