/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Program.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods and properties related to the execution of the program, including command-line argument handling delays, and termination.
 */

namespace SmallBasicOpenEditionDll.Classes
{
    /// <summary>Provides methods and properties related to the execution of the program, including command-line argument handling, delays, and termination.</summary>
    public static class Program
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


        /// <summary>Gets the number of command-line arguments passed to the program (excluding the program name).</summary>
        /// <value>The number of command-line arguments.</value>
        public static int ArgumentCount() => Environment.GetCommandLineArgs().Length - 1;

        /// <summary>Gets the directory where the program is being executed.</summary>
        /// <value>The base directory of the program.</value>
        public static string Directory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>Delays the program execution for the specified number of milliseconds.</summary>
        /// <param name="milliSeconds">The number of milliseconds to delay the program.</param>
        public static void Delay(int milliSeconds) => Thread.Sleep(milliSeconds);  // Can't be interrupted

        /// <summary>Pauses the program execution for the specified number of seconds. The sleep can be interrupted by a key press.</summary>
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

        /// <summary>Ends the program execution immediately.</summary>
        public static void End() => Environment.Exit(0);

        /// <summary>Retrieves the command-line argument at the specified index.</summary>
        /// <param name="index">The index of the command-line argument to retrieve (0-based).</param>
        /// <returns>The command-line argument at the specified index.</returns>
        public static string? GetArgument(int index)
        {
            int foo = ArgumentCount();

            if (index <= 0 || index > foo)
            {
                LastError = "Invalid argument index.";
                return null;
            }

            LastError = null;
            string[] args = Environment.GetCommandLineArgs();
            return index < 0 || index >= args.Length ? "" : args[index];
        }
    }
}
