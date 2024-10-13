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


using System;
using System.Runtime.InteropServices;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods and properties for interacting with the console text window, including window manipulation, cursor control, and reading/writing text.
    /// </summary>
    public static class TextWindow
    {
        // PInvoke constants for window states
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9; // This constant restores a minimized or hidden window

        // PInvoke function for ShowWindow
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // PInvoke function for getting the console window handle
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        private static ConsoleColor _foregroundColor = Console.ForegroundColor;
        private static ConsoleColor _backgroundColor = Console.BackgroundColor;

        /// <summary>
        /// Gets or sets the foreground color of the text in the console window.
        /// </summary>
        public static ConsoleColor ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                Console.ForegroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color of the console window.
        /// </summary>
        public static ConsoleColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Console.BackgroundColor = value;
                Console.Clear();  // Reset the window with the new background color
            }
        }

        /// <summary>
        /// Gets or sets the cursor's column position.
        /// </summary>
        public static int CursorLeft
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }

        /// <summary>
        /// Gets or sets the cursor's row position.
        /// </summary>
        public static int CursorTop
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }

        /// <summary>
        /// Gets or sets the title of the console window.
        /// </summary>
        public static string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        /// <summary>
        /// Shows and restores the text window if it is minimized or hidden.
        /// </summary>
        public static void Show()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_RESTORE);
        }

        /// <summary>
        /// Hides the text window.
        /// </summary>
        public static void Hide()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

        /// <summary>
        /// Clears all the content of the console text window.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Pauses execution and waits for user input, displaying a message.
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Pauses if the text window is visible, displaying a message.
        /// </summary>
        public static void PauseIfVisible()
        {
            Console.WriteLine("Pausing because text window is visible. Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Pauses execution without displaying a message, waiting for a key press.
        /// </summary>
        public static void PauseWithoutMessage()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// Reads a line of text from the console window.
        /// </summary>
        /// <returns>The string input from the console.</returns>
        public static string Read()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Reads a single key press from the console window.
        /// </summary>
        /// <returns>The character corresponding to the key pressed.</returns>
        public static char ReadKey()
        {
            return Console.ReadKey(true).KeyChar;
        }

        /// <summary>
        /// Reads a number from the console window.
        /// </summary>
        /// <returns>The number input from the console.</returns>
        /// <exception cref="FormatException">Thrown when input is not a valid number.</exception>
        public static double ReadNumber()
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

        /// <summary>
        /// Writes data to the console window, followed by a new line.
        /// </summary>
        /// <param name="data">The data to write.</param>
        public static void WriteLine(object data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// Writes data to the console window without appending a new line.
        /// </summary>
        /// <param name="data">The data to write.</param>
        public static void Write(object data)
        {
            Console.Write(data);
        }

        /// <summary>
        /// Simulates verifying access to the text window.
        /// </summary>
        /// <returns><c>true</c> if access is verified; otherwise, <c>false</c>.</returns>
        public static bool VerifyAccess()
        {
            // Simulating access verification
            return true;
        }
    }
}
