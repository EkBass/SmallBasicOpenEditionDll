/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Clock.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * The Clock class provides access to the system's date and time, offering properties to retrieve current time, date, and related information in different formats.
 */

namespace SmallBasicOpenEditionDll.Classes
{
    /// <summary>Provides access to the system clock to retrieve the current time, date, and related information.</summary>
    public static class Clock
    {
        // Backing field for LastError
        private static string? _lastError;

        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError
        {
            get => _lastError;
            set
            {
                // Only add a timestamp if the value is not null
                if (value != null)
                {
                    _lastError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}";
                }
                else
                {
                    _lastError = null;  // Set to null without timestamp
                }
            }
        }


        // Static readonly DateTimeOffset representing January 1, 2024, UTC
        private static readonly DateTimeOffset startOf2024 = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        // Store the program start time to calculate elapsed time later
        private static readonly DateTimeOffset programStart = DateTimeOffset.UtcNow;

        /// <summary>Gets the current system time as a string in "HH:mm:ss" format.</summary>
        public static string Time => DateTimeOffset.Now.ToString("HH:mm:ss");

        /// <summary>Gets the current system date as a string in "yyyy-MM-dd" format.</summary>
        public static string Date => DateTimeOffset.Now.ToString("yyyy-MM-dd");

        /// <summary>Gets the current system year as an integer.</summary>
        public static int Year => DateTimeOffset.Now.Year;

        /// <summary>Gets the current system month as an integer. </summary>
        public static int Month => DateTimeOffset.Now.Month;

        /// <summary>Gets the current day of the month as an integer.</summary>
        public static int Day => DateTimeOffset.Now.Day;

        /// <summary>Gets the current day of the week as a string.</summary>
        public static string WeekDay => DateTimeOffset.Now.DayOfWeek.ToString();

        /// <summary>Gets the current system hour as an integer.</summary>
        public static int Hour => DateTimeOffset.Now.Hour;

        /// <summary>Gets the current system minute as an integer.</summary>
        public static int Minute => DateTimeOffset.Now.Minute;

        /// <summary>Gets the current system second as an integer.</summary>
        public static int Second => DateTimeOffset.Now.Second;

        /// <summary>Gets the current system millisecond as an integer.</summary>
        public static int Millisecond => DateTimeOffset.Now.Millisecond;

        /// <summary>Gets the number of milliseconds that have elapsed since the program started.</summary>
        public static long ElapsedMillisecondsAfterStart()
        {
            TimeSpan elapsed = DateTimeOffset.UtcNow - programStart;
            return (long)elapsed.TotalMilliseconds;
        }

        /// <summary>Gets the number of milliseconds that have elapsed since January 1, 2024 (UTC).</summary>
        public static long ElapsedMilliseconds()
        {
            // Calculate the total milliseconds since January 1, 2024
            TimeSpan elapsed = DateTimeOffset.UtcNow - startOf2024;
            return (long)elapsed.TotalMilliseconds;
        }
    }
}