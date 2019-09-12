using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Setting
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<SettingValue<T>> GetAsync<T>(ISettingStoreContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync<T>(ISettingStoreContext context, T value);

    }
}
