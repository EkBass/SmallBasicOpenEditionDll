/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Clock.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 12.10.24 Kristian Virtanen
 * License: See license.txt
 */
 
using SmallBasicOpenEditionDll;
namespace SmallBasicOpenEditionDll
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWindow.WriteLine("Time is: " + Clock.Time);
            TextWindow.WriteLine("Date is: " + Clock.Date);
            TextWindow.WriteLine("Year is: " + Clock.Year);
            TextWindow.WriteLine("Month is: " + Clock.Month);
            TextWindow.WriteLine("Date is: " + Clock.Date);
            TextWindow.WriteLine("Weekday is: " + Clock.WeekDay);
            TextWindow.WriteLine("Hour is: " + Clock.Hour);
            TextWindow.WriteLine("Minute is: " + Clock.Minute);
            TextWindow.WriteLine("Second is: " + Clock.Second);
            TextWindow.WriteLine("Millisecond is: " + Clock.Millisecond);
            TextWindow.WriteLine("Elapsed milliseconds from year 1900 is: " + Clock.ElapsedMilliseconds);

        }
    }
}

/* OUTPUT
Time is: 18.11.16
Date is: 2024-10-12
Year is: 2024
Month is: 10
Date is: 2024-10-12
Weekday is: Saturday
Hour is: 18
Minute is: 11
Second is: 16
Millisecond is: 855
Elapsed milliseconds from year 1900 is: 3937734676855
*/