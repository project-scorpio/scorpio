using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.Modularity
{
    /// <summary>
    /// This class must be implemented by all module definition classes.
    /// </summary>
    /// <remarks>
    /// A module definition class is generally located in it's own assembly
    /// and implements some action in module events on application startup and shutdown.
    /// It also defines depended modules.
    /// </remarks>
    public abstract class ScorpioModule : IScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void PreConfigureServices(ConfigureServicesContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void ConfigureServices(ConfigureServicesContext context)
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void PostConfigureServices(ConfigureServicesContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IScorpioModule).GetTypeInfo().IsAssignableFrom(type);
        }

        internal static void CheckModuleType(Type moduleType)
        {
            if (!IsModule(moduleType))
            {
                throw new ArgumentException("Given type is not an Scorpio module: " + moduleType.AssemblyQualifiedName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void PreInitialize(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void Initialize(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void PostInitialize(ApplicationInitializationContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void Shutdown(ApplicationShutdownContext context)
        {
        }
    }
}
