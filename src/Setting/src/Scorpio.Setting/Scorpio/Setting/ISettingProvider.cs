using System.Threading.Tasks;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingProvider
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingDefinition"></param>
        /// <returns></returns>
        Task<SettingValue<T>> GetAsync<T>(SettingDefinition<T> settingDefinition);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingDefinition"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync<T>(SettingDefinition<T> settingDefinition, T value);
    }
}
