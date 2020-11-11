using System;

using Newtonsoft.Json;

using Scorpio.DependencyInjection;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonBackgroundJobSerializer : IBackgroundJobSerializer, ITransientDependency
    {

        /// <summary>
        /// 
        /// </summary>
        public JsonBackgroundJobSerializer()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string value, Type type) => JsonConvert.DeserializeObject(value, type);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value);
    }
}