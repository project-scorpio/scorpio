
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<string> GetOrNullAsync(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        Task<string> GetOrNullAsync(string name,  string providerName,  string providerKey, bool fallback = true);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<SettingValue>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        Task<List<SettingValue>> GetAllAsync( string providerName,  string providerKey, bool fallback = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="forceToSet"></param>
        /// <returns></returns>
        Task SetAsync( string name,  string value,  string providerName,  string providerKey, bool forceToSet = false);
    }
}
