/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_TextWindow.cs
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
            Console.WriteLine("TextWindow Class Test Application");

            // Test Case 1: Set and get the title of the console window
            Console.WriteLine("\nTest Case 1: Set and get console window title");
            TextWindow.Title = "Test Console Window";
            Console.WriteLine($"Console Title: {TextWindow.Title}");

            // Test Case 2: Change the text color
            Console.WriteLine("\nTest Case 2: Change text color");
            TextWindow.ForegroundColor = ConsoleColor.Green;
            TextWindow.WriteLine("This text should appear in green");

            // Test Case 3: Change the background color
            Console.WriteLine("\nTest Case 3: Change background color");
            TextWindow.BackgroundColor = ConsoleColor.DarkBlue;
            TextWindow.WriteLine("The background color should now be dark blue");

            // Test Case 4: Cursor control
            Console.WriteLine("\nTest Case 4: Cursor control");
            TextWindow.CursorTop = 10; // Set the cursor row position
            TextWindow.CursorLeft = 5;  // Set the cursor column position
            TextWindow.WriteLine("This text is written at row 10, column 5");

            // Test Case 5: Window state control
            Console.WriteLine("\nTest Case 5: Window state control");
            TextWindow.Pause();
            Console.WriteLine("Hiding textwindow...");
            Program.Sleep(3);
            TextWindow.Hide();
            Program.Sleep(3);
            Console.WriteLine("And lets get it back......");
            TextWindow.Show();

            // Test Case 6: Clear the console window
            Console.WriteLine("\nTest Case 6: Clear the console window");
            TextWindow.Pause();
            TextWindow.Clear();
            TextWindow.WriteLine("The console has been cleared");

            // Test Case 7: Pause and read input
            Console.WriteLine("\nTest Case 7: Pause and read input");
            TextWindow.Pause();
            TextWindow.Write("Enter some text: ");
            string inputText = TextWindow.Read();
            TextWindow.WriteLine($"You entered: {inputText}");

            // Test Case 8: Read a number
            Console.WriteLine("\nTest Case 8: Read a number");
            TextWindow.Write("Enter a number: ");
            double number = TextWindow.ReadNumber();
            TextWindow.WriteLine($"You entered the number: {number}");

            // Test Case 9: Read a key press
            Console.WriteLine("\nTest Case 9: Read a key press");
            TextWindow.WriteLine("Press any key to continue...");
            char keyPressed = TextWindow.ReadKey();
            TextWindow.WriteLine($"You pressed: {keyPressed}");

            // Test Case 10: Verify access to the text window
            Console.WriteLine("\nTest Case 10: Verify access to the text window");
            bool hasAccess = TextWindow.VerifyAccess();
            TextWindow.WriteLine($"Access to text window verified: {hasAccess}");

            Console.WriteLine("\nTextWindow Class Test Complete. Press any key to exit.");
            Console.ReadKey();

        }
    }
}