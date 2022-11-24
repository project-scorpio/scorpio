using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EventNameAttribute : Attribute, IEventNameProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public EventNameAttribute(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        public static string GetNameOrDefault<TEvent>()
        {
            return GetNameOrDefault(typeof(TEvent));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public static string GetNameOrDefault(Type eventType)
        {
            Check.NotNull(eventType, nameof(eventType));

            return eventType
                       .GetCustomAttributes(true)
                       .OfType<IEventNameProvider>()
                       .FirstOrDefault()
                       ?.GetName(eventType)
                   ?? eventType.FullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public string GetName(Type eventType)
        {
            return Name;
        }
    }
}
