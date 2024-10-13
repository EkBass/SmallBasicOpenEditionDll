/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Clock.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Updated: 14h October 2024 Kristian Virtanen
 * License: See license.txt
 * 
 * Description:
 * The Clock class provides access to the system's date and time, offering properties to retrieve current time, date, and related information in different formats.
 * It uses the system clock to return values since January 1, 1900.
 */

using System;

namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides access to the system clock to retrieve the current time, date, and related information.</summary>
    public static class Clock
    {
        /// <summary>Gets the current system time as a string in "HH:mm:ss" format.</summary>
        public static dynamic Time => DateTime.Now.ToString("HH:mm:ss");

        /// <summary>Gets the current system date as a string in "yyyy-MM-dd" format.</summary>
        public static dynamic Date => DateTime.Now.ToString("yyyy-MM-dd");

        /// <summary>Gets the current system year as an integer.</summary>
        public static dynamic Year => DateTime.Now.Year;

        /// <summary>Gets the current system month as an integer. </summary>
        public static dynamic Month => DateTime.Now.Month;

        /// <summary>Gets the current day of the month as an integer.</summary>
        public static dynamic Day => DateTime.Now.Day;

        /// <summary>Gets the current day of the week as a string.</summary>
        public static dynamic WeekDay => DateTime.Now.DayOfWeek.ToString();

        /// <summary>Gets the current system hour as an integer.</summary>
        public static dynamic Hour => DateTime.Now.Hour;

        /// <summary>Gets the current system minute as an integer.</summary>
        public static dynamic Minute => DateTime.Now.Minute;

        /// <summary>Gets the current system second as an integer.</summary>
        public static dynamic Second => DateTime.Now.Second;

        /// <summary>Gets the current system millisecond as an integer.</summary>
        public static dynamic Millisecond => DateTime.Now.Millisecond;

        /// <summary>Gets the number of milliseconds that have elapsed since January 1, 1900.</summary>
        public static dynamic ElapsedMilliseconds
        {
            get
            {
                DateTime start = new(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime now = DateTime.UtcNow;
                return (long)(now - start).TotalMilliseconds;
            }
        }
    }
}