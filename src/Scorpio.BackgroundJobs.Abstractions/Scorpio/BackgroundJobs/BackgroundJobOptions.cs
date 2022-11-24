using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundJobOptions
    {
        private readonly Dictionary<Type, BackgroundJobConfiguration> _jobConfigurationsByArgsType;
        private readonly Dictionary<string, BackgroundJobConfiguration> _jobConfigurationsByName;

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsJobExecutionEnabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public BackgroundJobOptions()
        {
            _jobConfigurationsByArgsType = new Dictionary<Type, BackgroundJobConfiguration>();
            _jobConfigurationsByName = new Dictionary<string, BackgroundJobConfiguration>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TArgs"></typeparam>
        /// <returns></returns>
        public BackgroundJobConfiguration GetJob<TArgs>() => GetJob(typeof(TArgs));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argsType"></param>
        /// <returns></returns>
        public BackgroundJobConfiguration GetJob(Type argsType)
        {
            var jobConfiguration = _jobConfigurationsByArgsType.GetOrDefault(argsType);

            if (jobConfiguration == null)
            {
                throw new ScorpioException("Undefined background job for the job args type: " + argsType.AssemblyQualifiedName);
            }

            return jobConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BackgroundJobConfiguration GetJob(string name)
        {
            var jobConfiguration = _jobConfigurationsByName.GetOrDefault(name);

            if (jobConfiguration == null)
            {
                throw new ScorpioException("Undefined background job for the job name: " + name);
            }

            return jobConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<BackgroundJobConfiguration> GetJobs() => _jobConfigurationsByArgsType.Values.ToImmutableList();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        public void AddJob<TJob>() => AddJob(typeof(TJob));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobType"></param>
        public void AddJob(Type jobType) => AddJob(new BackgroundJobConfiguration(jobType));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobConfiguration"></param>
        public void AddJob(BackgroundJobConfiguration jobConfiguration)
        {
            _jobConfigurationsByArgsType[jobConfiguration.ArgsType] = jobConfiguration;
            _jobConfigurationsByName[jobConfiguration.JobName] = jobConfiguration;
        }
    }
}