/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Program.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * Provides methods and properties related to the execution of the program, including command-line argument handling delays, and termination.
 */

using System;
using System.Threading;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods and properties related to the execution of the program, including command-line argument handling,
    /// delays, and termination.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Gets the number of command-line arguments passed to the program (excluding the program name).
        /// </summary>
        /// <value>The number of command-line arguments.</value>
        public static int ArgumentCount => Environment.GetCommandLineArgs().Length - 1;

        /// <summary>
        /// Gets the directory where the program is being executed.
        /// </summary>
        /// <value>The base directory of the program.</value>
        public static string Directory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Delays the program execution for the specified number of milliseconds.
        /// </summary>
        /// <param name="milliSeconds">The number of milliseconds to delay the program.</param>
        public static void Delay(int milliSeconds) => Thread.Sleep(milliSeconds);  // Can't be interrupted

        /// <summary>
        /// Pauses the program execution for the specified number of seconds. The sleep can be interrupted by a key press.
        /// </summary>
        /// <param name="seconds">The number of seconds to sleep.</param>
        public static void Sleep(int seconds)
        {
            for (int i = 0; i < seconds * 1000; i += 100)
            {
                if (Console.KeyAvailable)
                {
                    // Key press interrupts the sleep
                    Console.ReadKey(true);  // Consume the key press
                    return;
                }
                Thread.Sleep(100);  // Check every 100 milliseconds
            }
        }

        /// <summary>
        /// Ends the program execution immediately.
        /// </summary>
        public static void End() => Environment.Exit(0);

        /// <summary>
        /// Retrieves the command-line argument at the specified index.
        /// </summary>
        /// <param name="index">The index of the command-line argument to retrieve (0-based).</param>
        /// <returns>The command-line argument at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of range of the command-line arguments.</exception>
        public static string GetArgument(int index)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (index >= 0 && index < args.Length)
            {
                return args[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Invalid argument index.");
            }
        }
    }
}
