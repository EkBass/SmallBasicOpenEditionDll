/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Mouse.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * The Mouse class provides a set of methods and properties to interact with the system mouse
 */

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods and properties for interacting with the mouse,
    /// including getting or setting the mouse position, checking button states, and controlling cursor visibility.
    /// </summary>
    public static class Mouse
    {
        // Import for cursor visibility control from user32.dll
        [DllImport("user32.dll")]
        private static extern int ShowCursor(bool bShow);

        /// <summary>Gets or sets the X coordinate of the mouse cursor on the screen.</summary>
        /// <returns>
        /// The X coordinate of the mouse cursor in pixels.
        /// </returns>
        public static dynamic MouseX => Cursor.Position.X;

        /// <summary>Gets or sets the X coordinate of the mouse cursor on the screen.</summary>
        /// <param name="value">
        /// The X coordinate of the mouse cursor in pixels.
        /// </param>
        public static void SetMouseX(dynamic value) => Cursor.Position = new System.Drawing.Point(value, Cursor.Position.Y);

        /// <summary>Gets or sets the Y coordinate of the mouse cursor on the screen.</summary>
        /// <returns>
        /// The Y coordinate of the mouse cursor in pixels.
        /// </returns>
        public static dynamic GetMouseY() => Cursor.Position.Y;

        /// <summary>Gets or sets the Y coordinate of the mouse cursor on the screen.</summary>
        /// <param name="value">
        /// The Y coordinate of the mouse cursor in pixels.
        /// </param>
        public static void SetMouseY(dynamic value) => Cursor.Position = new System.Drawing.Point(Cursor.Position.X, value);

        /// <summary>Gets a value indicating whether the left mouse button is currently pressed.</summary>
        /// <value>
        /// <c>true</c> if the left mouse button is down; otherwise, <c>false</c>.
        /// </value>
        public static dynamic IsLeftButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Left);

        /// <summary>Gets a value indicating whether the right mouse button is currently pressed.</summary>
        /// <value>
        /// <c>true</c> if the right mouse button is down; otherwise, <c>false</c>.
        /// </value>
        public static dynamic IsRightButtonDown => Control.MouseButtons.HasFlag(MouseButtons.Right);

        /// <summary>Hides the mouse cursor from the screen.</summary>
        public static void HideCursor() => ShowCursor(false);

        /// <summary>Shows the mouse cursor on the screen.</summary>
        public static void ShowCursor() => ShowCursor(true);
    }
}
