
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingEncryptionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinition"></param>
        /// <param name="plainValue"></param>
        /// <returns></returns>
        
        string Encrypt(SettingDefinition settingDefinition,  string plainValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingDefinition"></param>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        
        string Decrypt(SettingDefinition settingDefinition,  string encryptedValue);
    }
}
