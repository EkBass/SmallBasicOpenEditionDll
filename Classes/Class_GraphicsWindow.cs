﻿/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_GraphicsWindow.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
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
    public static class GraphicsWindow
    {
        public static Form? graphicsForm;
        public static Panel? drawingPanel;
        public static Graphics? graphics;
        public static Color backgroundColor = Color.White;
        public static Color penColor = Color.Black;
        public static Color brushColor = Color.Black;
        public static int penWidth = 1;
        public static int fontSize;
        public static Font font = new("Arial", 12);
        public static bool canResize = false;

        // Backing fields for width and height
        private static int _width = 800;
        private static int _height = 600;

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


        // --- Event Handling ---
        public static event EventHandler<KeyEventArgs>? KeyDown;
        public static event EventHandler<KeyEventArgs>? KeyUp;
        public static event EventHandler<MouseEventArgs>? MouseDown;
        public static event EventHandler<MouseEventArgs>? MouseUp;
        public static event EventHandler<MouseEventArgs>? MouseMove;

        // --- Properties ---
        public static int Width
        {
            get => _width;
            set
            {
                _width = value;
                ResizeWindow();
            }
        }

        public static int Height
        {
            get => _height;
            set
            {
                _height = value;
                ResizeWindow();
            }
        }

        public static string BackgroundColor
        {
            get => backgroundColor.Name;
            set
            {
                LastError = null;
                try
                {
                    backgroundColor = Color.FromName(value);
                    if (drawingPanel != null)
                    {
                        drawingPanel.BackColor = backgroundColor;
                    }
                }
                catch
                {
                    LastError = $"Invalid background color: {value}";
                }
            }
        }

        public static string PenColor
        {
            get => penColor.Name;
            set
            {
                LastError = null;
                try
                {
                    penColor = Color.FromName(value);
                }
                catch
                {
                    LastError = $"Invalid pen color: {value}";
                }
            }
        }

        public static string BrushColor
        {
            get => brushColor.Name;
            set
            {
                LastError = null;
                try
                {
                    brushColor = Color.FromName(value);
                }
                catch
                {
                    LastError = $"Invalid brush color: {value}";
                }
            }
        }

        public static int PenWidth
        {
            get => penWidth;
            set
            {
                LastError = null;
                try
                {
                    if (value > 0)
                    {
                        penWidth = value;
                    }
                    else
                    {
                        LastError = $"Invalid pen width: {value}";
                    }
                }
                catch
                {
                    LastError = $"Error setting pen width: {value}";
                }
            }
        }

        public static string FontName
        {
            get => font.Name;
            set
            {
                LastError = null;
                try
                {
                    font = new Font(value, font.Size, font.Style);
                }
                catch
                {
                    LastError = $"Invalid font name: {value}";
                }
            }
        }

        public static int FontSize
        {
            get => (int)font.Size; // Cast the float to int when returning

            set
            {
                LastError = null;
                try
                {
                    if (value > 0)
                    {
                        font = new Font(font.Name, (float)value, font.Style); // Cast int to float when setting the font size
                    }
                    else
                    {
                        LastError = $"Invalid font size: {value}";
                    }
                }
                catch
                {
                    LastError = $"Error setting font size: {value}";
                }
            }
        }


        // --- Methods ---
        public static bool Show()
        {
            LastError = null;
            try
            {
                if (graphicsForm == null)
                {
                    graphicsForm = new Form
                    {
                        Text = "Graphics Window",
                        Size = new Size(Width, Height),
                        FormBorderStyle = canResize ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog
                    };

                    drawingPanel = new Panel { Dock = DockStyle.Fill, BackColor = backgroundColor };
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

                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool Hide()
        {
            LastError = null;
            try
            {
                graphicsForm?.Hide();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // Drawing methods with null-checks
        public static bool DrawRectangle(int x, int y, int width, int height)
        {
            if (!CheckGraphics()) return false;
            LastError = null;

            try
            {
                using Pen pen = new(penColor, penWidth);
                graphics?.DrawRectangle(pen, x, y, width, height);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool FillRectangle(int x, int y, int width, int height)
        {
            if (!CheckGraphics()) return false;

            try
            {
                LastError = null;
                using Brush brush = new SolidBrush(brushColor);
                graphics?.FillRectangle(brush, x, y, width, height);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // Additional methods like DrawEllipse, FillEllipse, etc. would go here...

        private static bool CheckGraphics()
        {
            if (graphics == null)
            {
                LastError = "Graphics object is not initialized.";
                return false;
            }
            return true;
        }

        private static bool ResizeWindow()
        {
            if (!CheckGraphics()) return false;
            LastError = null;

            try
            {
                graphicsForm.Size = new Size(_width, _height);
                graphicsForm.Refresh();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }
    }
}
