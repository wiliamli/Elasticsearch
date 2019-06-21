using System;

namespace Jwell.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToFullTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// yyyy/MM/dd HH:mm
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToFullTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return dateTime.Value.ToFullTimeString();
        }

        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

    }
}
