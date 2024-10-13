/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Timer.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Tested 13.10.24 Kristian Virtanen
 * License: See license.txt
 */

namespace SmallBasicOpenEditionDll
{
    class Program2
    {
        static void Main(string[] args)
        {

            // Subscribe to the Tick event
            Timer.Tick += OnTick;

            // Set the interval to 1000 milliseconds (1 second)
            Timer.Interval = 1000;

            Console.WriteLine("Timer started. Press any key to pause the timer.");

            // Start the timer
            Timer.Resume();

            // Wait for user input to pause the timer
            Console.ReadKey();
            Console.WriteLine("Timer paused. Press any key to resume the timer.");
            Timer.Pause();

            // Wait for user input to resume the timer
            Console.ReadKey();
            Console.WriteLine("Timer resumed. Press any key to exit.");
            Timer.Resume();

            // Wait for user input to stop and exit the program
            Console.ReadKey();
            Timer.Pause();
            Console.WriteLine("Timer stopped. Exiting...");

            // Event handler for timer ticks
            static void OnTick()
            {
                Console.WriteLine("Tick at: " + DateTime.Now);
            }
        }
    }
}

/*
 * Timer started. Press any key to pause the timer.
Tick at: 13.10.2024 14.42.54
Tick at: 13.10.2024 14.42.55
Tick at: 13.10.2024 14.42.56
Tick at: 13.10.2024 14.42.57
Tick at: 13.10.2024 14.42.58
Tick at: 13.10.2024 14.42.59
Timer paused. Press any key to resume the timer.
Timer resumed. Press any key to exit.
Tick at: 13.10.2024 14.43.02
Timer stopped. Exiting...
*/