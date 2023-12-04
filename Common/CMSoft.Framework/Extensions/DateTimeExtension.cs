using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSoft.Framework.Extensions
{
    /// <summary>
    /// Extension class to datetime objects
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Convert Unix date to a datetime format
        /// </summary>
        /// <param name="epochMilliseconds">Target</param>
        /// <returns>Date in <see cref="DateTime"/> format</returns>
        public static DateTime GetDateTimeFromUnix(this long epochMilliseconds)
        {
            DateTimeOffset datatime = DateTimeOffset.FromUnixTimeMilliseconds(epochMilliseconds);
            return datatime.DateTime;
        }

        /// <summary>
        /// Convert  datetime format to a Unix date
        /// </summary>
        /// <param name="date">Target</param>
        /// <returns>Date in unix format</returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalMilliseconds);
        }
    }
}
