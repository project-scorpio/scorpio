using System.Threading.Tasks;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingManager
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync<T>(string name, T value, string providerName = "Default");
    }
}
