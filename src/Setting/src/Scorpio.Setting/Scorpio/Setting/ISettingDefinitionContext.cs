namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SettingDefinition GetOrNull(string name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinitions"></param>
        void Add(params SettingDefinition[] settingDefinitions);
    }
}
