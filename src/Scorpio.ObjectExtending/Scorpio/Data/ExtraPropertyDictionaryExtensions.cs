using System;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtraPropertyDictionaryExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extraPropertyDictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this ExtraPropertyDictionary extraPropertyDictionary, string key)
            where T : Enum
        {
            if (extraPropertyDictionary[key].GetType() == typeof(T))
            {
                return (T)extraPropertyDictionary[key];
            }

            extraPropertyDictionary[key] = Enum.Parse(typeof(T), extraPropertyDictionary[key].ToString(), ignoreCase: true);
            return (T)extraPropertyDictionary[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extraPropertyDictionary"></param>
        /// <param name="key"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static object ToEnum(this ExtraPropertyDictionary extraPropertyDictionary, string key, Type enumType)
        {
            if (!enumType.IsEnum || extraPropertyDictionary[key].GetType() == enumType)
            {
                return extraPropertyDictionary[key];
            }

            extraPropertyDictionary[key] = Enum.Parse(enumType, extraPropertyDictionary[key].ToString(), ignoreCase: true);
            return extraPropertyDictionary[key];
        }
    }
}
