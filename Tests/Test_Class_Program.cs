/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Program.cs
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
            // 1. Use Program.ArgumentCount to get the number of command-line arguments (excluding the program name)
            TextWindow.WriteLine($"Argument Count: {Program.ArgumentCount}");

            // 2. Use Program.GetArgument to retrieve each command-line argument
            for (int i = 1; i <= Program.ArgumentCount; i++)
            {
                // Environment.GetCommandLineArgs() includes the program name at index 0
                string argument = Program.GetArgument(i);
                TextWindow.WriteLine($"Argument {i}: {argument}");
            }

            // 3. Use Program.Directory to get the execution directory
            TextWindow.WriteLine($"\nExecution Directory: {Program.Directory}");

            // 4. Use Program.Delay to pause execution for a specified number of milliseconds
            TextWindow.WriteLine("\nDelaying for 1000 milliseconds...");
            Program.Delay(1000);
            TextWindow.WriteLine("Delay complete.");

            // 5. Use Program.Sleep to pause execution for a specified number of seconds, can be interrupted by key press
            TextWindow.WriteLine("\nSleeping for 5 seconds (press any key to interrupt)...");
            Program.Sleep(5);
            TextWindow.WriteLine("Sleep complete or interrupted by key press.");

            // 6. Use Program.End to terminate the program
            TextWindow.WriteLine("\nProgram will end now.");
            Program.End();

            // This line will not be reached because Program.End exits the application
            TextWindow.WriteLine("This message will not be displayed.");
        }
    }
}