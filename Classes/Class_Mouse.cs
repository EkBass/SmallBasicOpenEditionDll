/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Mouse.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * The Mouse class provides a set of methods and properties to interact with the system mouse
 */

using System.Runtime.InteropServices;

namespace SmallBasicOpenEditionDll
{
    public static class Mouse
    {

        #pragma warning disable SYSLIB1054
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int ShowCursor(bool bShow);

        /// <summary>Gets or sets the X coordinate of the mouse cursor on the screen.</summary>
        public static int MouseX
        {
            get => Cursor.Position.X;
            set => Cursor.Position = new System.Drawing.Point(value, Cursor.Position.Y);
        }

        /// <summary>Gets or sets the Y coordinate of the mouse cursor on the screen.</summary>
        public static int MouseY
        {
            get => Cursor.Position.Y;
            set => Cursor.Position = new System.Drawing.Point(Cursor.Position.X, value);
        }

        public static bool IsLeftButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Left);

        public static bool IsRightButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Right);

        public static void HideCursor()
        {
            int result = ShowCursor(false);
            if (result < 0)
            {
                // Optionally log or handle this error
            }
        }

        public static void ShowCursor()
        {
            int result = ShowCursor(true);
            if (result < 0)
            {
                // Optionally log or handle this error
            }
        }
    }
}
