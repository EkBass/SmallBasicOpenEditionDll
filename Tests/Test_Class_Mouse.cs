/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Test_Class_Mouse.cs
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
            // 1. Get the current mouse X and Y position
            TextWindow.WriteLine("Current Mouse Position:");
            TextWindow.WriteLine($"X: {Mouse.MouseX}, Y: {Mouse.GetMouseY()}");

            // 2. Set the mouse position to the center of the screen (arbitrary example)
            TextWindow.WriteLine("\nMoving the mouse cursor to (500, 500)...");
            Mouse.SetMouseX(500);
            Mouse.SetMouseY(500);
            Program.Sleep(5);  // Sleep for 1 second so you can see the movement
            TextWindow.WriteLine($"New Mouse Position: X: {Mouse.MouseX}, Y: {Mouse.GetMouseY()}");

            // 3. Check if the left or right mouse buttons are down
            TextWindow.WriteLine("\nChecking mouse button states...");
            if (Mouse.IsLeftButtonDown)
            {
                TextWindow.WriteLine("Left mouse button is down.");
            }
            else
            {
                TextWindow.WriteLine("Left mouse button is not pressed.");
            }

            if (Mouse.IsRightButtonDown)
            {
                TextWindow.WriteLine("Right mouse button is down.");
            }
            else
            {
                TextWindow.WriteLine("Right mouse button is not pressed.");
            }

            // 4. Hide the cursor for 2 seconds, then show it again
            TextWindow.WriteLine("\nHiding the mouse cursor for 2 seconds...");
            Mouse.HideCursor();
            Program.Sleep(5);  // Wait for 2 seconds
            TextWindow.WriteLine("Showing the mouse cursor again.");
            Mouse.ShowCursor();

            // 5. Wait for user input to end the program
            TextWindow.WriteLine("\nPress any key to exit...");
            TextWindow.ReadKey();
        }
    }
}

/* OUTPUT
# Works fine
*/