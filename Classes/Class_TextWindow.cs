/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_TextWindow.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 12th October 2024
 * License: See license.txt
 * 
 * Description:
 * Provides methods and properties for interacting with the console text window, including window manipulation, cursor control, and reading/writing text.
 */


using System.Runtime.InteropServices;
using System;
namespace SmallBasicOpenEditionDll
{
    public static class TextWindow
{
    // PInvoke constants for window states
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    private const int SW_MINIMIZE = 6;
    private const int SW_MAXIMIZE = 3;
    private const int SW_RESTORE = 9;

    // PInvoke function for ShowWindow
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    // PInvoke function to check the window's visibility
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    // PInvoke function for getting the console window handle
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();

    private static ConsoleColor _foregroundColor = Console.ForegroundColor;
    private static ConsoleColor _backgroundColor = Console.BackgroundColor;

    /// <summary>Gets or sets the foreground color of the text in the console window.</summary>
    public static dynamic ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                // Ensure that the value being set is a valid ConsoleColor
                if (value is ConsoleColor)
                {
                    _foregroundColor = value;
                    Console.ForegroundColor = value;
                }
                else
                {
                    throw new ArgumentException("Invalid color assigned. Must be of type ConsoleColor.");
                }
            }
        }


        /// <summary>Gets or sets the background color of the console window.</summary>
        public static dynamic BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                // Ensure that the value being set is a valid ConsoleColor
                if (value is ConsoleColor)
                {
                    _backgroundColor = value;
                    Console.BackgroundColor = value;
                }
                else
                {
                throw new ArgumentException("Invalid color assigned. Must be of type ConsoleColor.");
                }
            }
        }

        /// <summary>Gets or sets the cursor's column position.</summary>
        public static dynamic CursorLeft
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }

        /// <summary>Gets or sets the cursor's row position.</summary>
        public static dynamic CursorTop
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }

        /// <summary>Gets or sets the title of the console window.</summary>
        public static dynamic Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        /// <summary>Shows and restores the text window if it is minimized or hidden.</summary>
        public static void Show()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_RESTORE);
        }

        /// <summary>Hides the text window.</summary>
        public static void Hide()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

        /// <summary>Clears all the content of the console text window.</summary>
        public static void Clear() => Console.Clear();

        /// <summary>Pauses execution and waits for user input, displaying a message.</summary>
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>Pauses if the text window is visible, displaying a message.</summary>
        public static dynamic PauseIfVisible()
        {
            IntPtr consoleHandle = GetConsoleWindow();
            if (consoleHandle != IntPtr.Zero && IsWindowVisible(consoleHandle))
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return true;
            }
            return false;
        }

        /// <summary>Pauses execution without displaying a message, waiting for a key press.</summary>
        public static void PauseWithoutMessage() => Console.ReadKey();

        /// <summary>Reads a line of text from the console window.</summary>
        /// <returns>The string input from the console.</returns>
        public static dynamic Read() => Console.ReadLine() ?? string.Empty;

        /// <summary>Reads a single key press from the console window.</summary>
        /// <returns>The character corresponding to the key pressed.</returns>
        public static dynamic ReadKey() => Console.ReadKey(true).KeyChar;

        /// <summary>Reads a number from the console window.</summary>
        /// <returns>The number input from the console.</returns>
        /// <exception cref="FormatException">Thrown when input is not a valid number.</exception>
        public static dynamic ReadNumber()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double number))
                {
                    return number;
                }
                Console.WriteLine("Invalid number, please try again.");
            }
        }

        /// <summary>Writes data to the console window, followed by a new line.</summary>
        /// <param name="data">The data to write.</param>
        public static void WriteLine(object data) => Console.WriteLine(data);

        /// <summary>Writes data to the console window without appending a new line.</summary>
        /// <param name="data">The data to write.</param>
        public static void Write(object data) => Console.Write(data);

        /// <summary>Simulates verifying access to the text window.</summary>
        /// <returns><c>true</c> if access is verified; otherwise, <c>false</c>.</returns>
        public static dynamic VerifyAccess()
        {
            // Get the console window handle and check if it is valid and visible
            IntPtr consoleHandle = GetConsoleWindow();
            if (consoleHandle != IntPtr.Zero && IsWindowVisible(consoleHandle))
            {
                return true; // Console window is accessible
            }
            else
            {
                return false; // Console window is either not present or not visible
            }
        }
    }
}
