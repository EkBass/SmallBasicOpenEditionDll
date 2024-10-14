/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Mouse.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
  * License: See license.txt
 * 
 * Description:
 * The Mouse class provides a set of methods and properties to interact with the system mouse
 */

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    public static class Mouse
    {
        [DllImport("user32.dll")]
        private static extern int ShowCursor(bool bShow);

        /// <summary>Gets or sets the X coordinate of the mouse cursor on the screen.</summary>
        public static dynamic MouseX
        {
            get => Cursor.Position.X;
            set => Cursor.Position = new System.Drawing.Point(value, Cursor.Position.Y);
        }

        /// <summary>Gets or sets the Y coordinate of the mouse cursor on the screen.</summary>
        public static dynamic MouseY
        {
            get => Cursor.Position.Y;
            set => Cursor.Position = new System.Drawing.Point(Cursor.Position.X, value);
        }

        public static dynamic IsLeftButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Left);

        public static dynamic IsRightButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Right);

        public static void HideCursor() => ShowCursor(false);

        public static void ShowCursor() => ShowCursor(true);
    }
}

