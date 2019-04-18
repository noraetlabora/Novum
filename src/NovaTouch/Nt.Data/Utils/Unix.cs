using System;

namespace Nt.Data.Utils
{
    /// <summary>
    /// Helper class for Unix time conversion
    /// </summary>
    public static class Unix
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int TimestampNow()
        {
            return (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}