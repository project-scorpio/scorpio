using System;
using System.Linq;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobNameAttribute : Attribute, IBackgroundJobNameProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public BackgroundJobNameAttribute(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJobArgs"></typeparam>
        /// <returns></returns>
        public static string GetName<TJobArgs>()
        {
            return GetName(typeof(TJobArgs));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobArgsType"></param>
        /// <returns></returns>
        public static string GetName(Type jobArgsType)
        {
            Check.NotNull(jobArgsType, nameof(jobArgsType));

            return jobArgsType
                       .GetCustomAttributes(true)
                       .OfType<IBackgroundJobNameProvider>()
                       .FirstOrDefault()
                       ?.Name
                   ?? jobArgsType.FullName;
        }
    }
}
