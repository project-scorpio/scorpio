using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public class Clock : IClock, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        protected ClockOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public Clock(IOptions<ClockOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Now => Options.Kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTimeKind Kind => Options.Kind;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool SupportsMultipleTimezone => Options.Kind == DateTimeKind.Utc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public virtual DateTime Normalize(DateTime dateTime)
        {
            if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind)
            {
                return dateTime;
            }

            if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return DateTime.SpecifyKind(dateTime, Kind);
        }
    }
}
