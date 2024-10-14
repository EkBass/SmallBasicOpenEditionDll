/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Clock.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
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
        public static string Time => DateTime.Now.ToString("HH:mm:ss");

        /// <summary>Gets the current system date as a string in "yyyy-MM-dd" format.</summary>
        public static string Date => DateTime.Now.ToString("yyyy-MM-dd");

        /// <summary>Gets the current system year as an integer.</summary>
        public static int Year => DateTime.Now.Year;

        /// <summary>Gets the current system month as an integer. </summary>
        public static int Month => DateTime.Now.Month;

        /// <summary>Gets the current day of the month as an integer.</summary>
        public static int Day => DateTime.Now.Day;

        /// <summary>Gets the current day of the week as a string.</summary>
        public static string WeekDay => DateTime.Now.DayOfWeek.ToString();

        /// <summary>Gets the current system hour as an integer.</summary>
        public static int Hour => DateTime.Now.Hour;

        /// <summary>Gets the current system minute as an integer.</summary>
        public static int Minute => DateTime.Now.Minute;

        /// <summary>Gets the current system second as an integer.</summary>
        public static int Second => DateTime.Now.Second;

        /// <summary>Gets the current system millisecond as an integer.</summary>
        public static int Millisecond => DateTime.Now.Millisecond;

		/// <summary>Gets the number of milliseconds that have elapsed since January 1, 2024.</summary>
		public static int ElapsedMilliseconds
		{
			get
			{
                DateTime start = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				DateTime now = DateTime.UtcNow;
				return (int)(now - start).TotalMilliseconds;
			}
		}

    }
}