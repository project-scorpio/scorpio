﻿using System;

namespace Scorpio.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public class ClockOptions
    {
        /// <summary>
        /// Default: <see cref="DateTimeKind.Unspecified"/>
        /// </summary>
        public DateTimeKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ClockOptions() => Kind = DateTimeKind.Unspecified;
    }
}
