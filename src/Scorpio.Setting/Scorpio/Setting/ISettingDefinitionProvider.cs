using Scorpio.DependencyInjection;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingDefinitionProvider : ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Define(ISettingDefinitionContext context);
    }
}
