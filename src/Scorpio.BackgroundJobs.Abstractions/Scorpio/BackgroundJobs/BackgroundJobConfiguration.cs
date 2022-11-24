using System;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public Type ArgsType { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type JobType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string JobName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobType"></param>
        public BackgroundJobConfiguration(Type jobType)
        {
            JobType = jobType;
            ArgsType = BackgroundJobArgsHelper.GetJobArgsType(jobType);
            JobName = BackgroundJobNameAttribute.GetName(ArgsType);
        }
    }
}