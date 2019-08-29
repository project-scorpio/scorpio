namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationInitializationContext"></param>
        void InitializeModules(ApplicationInitializationContext applicationInitializationContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationShutdownContext"></param>
        void ShutdownModules(ApplicationShutdownContext applicationShutdownContext);
    }
}