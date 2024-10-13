/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_GraphicsWindow.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * GraphicsWindow provides functionality for creating, customizing, and managing a graphical window.
 * The window's properties, such as background color, dimensions, and resizability, can be controlled through various methods and properties.
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods and properties to create and manage a graphics window where users can draw shapes,
    /// handle input events, and customize the visual appearance of the window.
    /// </summary>
    public static class GraphicsWindow
    {
        public static Form? graphicsForm;
        public static Panel? drawingPanel;
        public static Graphics? graphics;
        public static Color backgroundColor = Color.White;
        public static Color penColor = Color.Black;
        public static Color brushColor = Color.Black;
        public static int penWidth = 1;
        public static Font font = new("Arial", 12);
        public static bool canResize = false;

        // Backing fields for width and height
        private static int _width = 800;
        private static int _height = 600;

        /// <summary>
        /// Occurs when a key is pressed while the graphics window has focus.
        /// </summary>
        public static event EventHandler<KeyEventArgs>? KeyDown;

        /// <summary>
        /// Occurs when a key is released while the graphics window has focus.
        /// </summary>
        public static event EventHandler<KeyEventArgs>? KeyUp;

        /// <summary>
        /// Occurs when the mouse button is pressed down in the graphics window.
        /// </summary>
        public static event EventHandler<MouseEventArgs>? MouseDown;

        /// <summary>
        /// Occurs when the mouse button is released in the graphics window.
        /// </summary>
        public static event EventHandler<MouseEventArgs>? MouseUp;

        /// <summary>
        /// Occurs when the mouse is moved within the graphics window.
        /// </summary>
        public static event EventHandler<MouseEventArgs>? MouseMove;

        /// <summary>
        /// Gets or sets the width of the graphics window.
        /// </summary>
        public static int Width
        {
            get => _width;
            set
            {
                _width = value;
                ResizeWindow();
            }
        }

        /// <summary>
        /// Gets or sets the height of the graphics window.
        /// </summary>
        public static int Height
        {
            get => _height;
            set
            {
                _height = value;
                ResizeWindow();
            }
        }

        /// <summary>
        /// Displays the graphics window. Initializes the window if it hasn't been created yet.
        /// </summary>
        public static void Show()
        {
            if (graphicsForm == null)
            {
                graphicsForm = new Form
                {
                    Text = "Graphics Window",
                    Size = new Size(Width, Height),
                    FormBorderStyle = canResize ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog
                };

                drawingPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = backgroundColor
                };

                graphicsForm.Controls.Add(drawingPanel);
                graphicsForm.Show();

                graphics = drawingPanel.CreateGraphics();

                // Event handling
                graphicsForm.KeyDown += (s, e) => KeyDown?.Invoke(s, e);
                graphicsForm.KeyUp += (s, e) => KeyUp?.Invoke(s, e);
                drawingPanel.MouseDown += (s, e) => MouseDown?.Invoke(s, e);
                drawingPanel.MouseUp += (s, e) => MouseUp?.Invoke(s, e);
                drawingPanel.MouseMove += (s, e) => MouseMove?.Invoke(s, e);
            }
            else
            {
                graphicsForm.Show();
            }
        }

        /// <summary>
        /// Hides the graphics window.
        /// </summary>
        public static void Hide()
        {
            graphicsForm?.Hide();
        }

        /// <summary>
        /// Gets or sets the background color of the graphics window.
        /// </summary>
        public static string BackgroundColor
        {
            get => backgroundColor.Name;
            set
            {
                backgroundColor = Color.FromName(value);
                if (drawingPanel != null)
                {
                    drawingPanel.BackColor = backgroundColor;
                }
            }
        }

        /// <summary>
        /// Gets or sets the pen color for drawing shapes.
        /// </summary>
        public static string PenColor
        {
            get => penColor.Name;
            set => penColor = Color.FromName(value);
        }

        /// <summary>
        /// Gets or sets the brush color used for filling shapes.
        /// </summary>
        public static string BrushColor
        {
            get => brushColor.Name;
            set => brushColor = Color.FromName(value);
        }

        /// <summary>
        /// Gets or sets the width of the pen used for drawing shapes.
        /// </summary>
        public static int PenWidth
        {
            get => penWidth;
            set => penWidth = value;
        }

        /// <summary>
        /// Gets or sets the name of the font used for drawing text.
        /// </summary>
        public static string FontName
        {
            get => font.Name;
            set => font = new Font(value, font.Size, font.Style);
        }

        /// <summary>
        /// Gets or sets the size of the font used for drawing text.
        /// </summary>
        public static int FontSize
        {
            get => (int)font.Size;
            set => font = new Font(font.Name, value, font.Style);
        }

        /// <summary>
        /// Draws a rectangle at the specified coordinates with the given dimensions.
        /// </summary>
        /// <param name="x">The X coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="y">The Y coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public static void DrawRectangle(int x, int y, int width, int height)
        {
            using Pen pen = new(penColor, penWidth);
            graphics?.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>
        /// Fills a rectangle at the specified coordinates with the given dimensions.
        /// </summary>
        /// <param name="x">The X coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="y">The Y coordinate of the upper-left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public static void FillRectangle(int x, int y, int width, int height)
        {
            using Brush brush = new SolidBrush(brushColor);
            graphics?.FillRectangle(brush, x, y, width, height);
        }

        /// <summary>
        /// Draws an ellipse at the specified coordinates with the given dimensions.
        /// </summary>
        /// <param name="x">The X coordinate of the upper-left corner of the bounding rectangle.</param>
        /// <param name="y">The Y coordinate of the upper-left corner of the bounding rectangle.</param>
        /// <param name="width">The width of the bounding rectangle.</param>
        /// <param name="height">The height of the bounding rectangle.</param>
        public static void DrawEllipse(int x, int y, int width, int height)
        {
            using Pen pen = new(penColor, penWidth);
            graphics?.DrawEllipse(pen, x, y, width, height);
        }

        /// <summary>
        /// Fills an ellipse at the specified coordinates with the given dimensions.
        /// </summary>
        /// <param name="x">The X coordinate of the upper-left corner of the bounding rectangle.</param>
        /// <param name="y">The Y coordinate of the upper-left corner of the bounding rectangle.</param>
        /// <param name="width">The width of the bounding rectangle.</param>
        /// <param name="height">The height of the bounding rectangle.</param>
        public static void FillEllipse(int x, int y, int width, int height)
        {
            using Brush brush = new SolidBrush(brushColor);
            graphics?.FillEllipse(brush, x, y, width, height);
        }

        /// <summary>
        /// Draws text at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate where the text will start.</param>
        /// <param name="y">The Y coordinate where the text will start.</param>
        /// <param name="text">The text to be drawn.</param>
        public static void DrawText(int x, int y, string text)
        {
            using Brush brush = new SolidBrush(penColor);
            graphics?.DrawString(text, font, brush, x, y);
        }

        /// <summary>
        /// Clears the graphics window, filling it with the background color.
        /// </summary>
        public static void Clear()
        {
            graphics?.Clear(backgroundColor);
        }

        /// <summary>
        /// Gets or sets whether the graphics window can be resized by the user.
        /// </summary>
        public static bool CanResize
        {
            get => canResize;
            set
            {
                canResize = value;
                if (graphicsForm != null)
                {
                    graphicsForm.FormBorderStyle = canResize ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog;
                }
            }
        }

        // Resizes the graphics window and refreshes its content.
        private static void ResizeWindow()
        {
            if (graphicsForm != null)
            {
                graphicsForm.Size = new Size(_width, _height);
                graphicsForm.Refresh();
                Console.WriteLine($"Window resized to Width: {_width}, Height: {_height}");
            }
        }
    }
}