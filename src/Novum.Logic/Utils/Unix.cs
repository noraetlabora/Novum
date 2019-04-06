using System;

namespace Novum.Logic.Utils
{
    /// <summary>
    /// Helper class for Unix time conversion
    /// </summary>
    internal static class Unix
    {
        private static readonly long UnixStartTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long Timestamp(DateTime date)
        {
            var timespan = new TimeSpan(date.Ticks - UnixStartTimeTicks);
            return (long)timespan.TotalSeconds;
        }

    }
}