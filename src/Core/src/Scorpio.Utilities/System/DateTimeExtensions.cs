using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="dateTime">The Date object to be converted</param>
        /// <returns> The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var offset = new DateTimeOffset(dateTime);
            return offset.ToUnixTimeSeconds();
        }


    }
}
