/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_GraphicsWindow.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * GraphicsWindow provides functionality for creating, customizing, and managing a graphical window.
 * The window's properties, such as background color, dimensions, and resizability, can be controlled through various methods and properties.
*/

namespace SmallBasicOpenEditionDll.Classes
{
    public static class GraphicsWindow
    {

        // Use a lock object for thread-safety when accessing fields
        private static readonly object lockObj = new();

        private static string? _lastError;
        /// <summary>Stores the last error msg as string, if any. null by default</summary>
        public static string? LastError
        {
            get => _lastError;
            set
            {
                lock (lockObj)
                {
                    _lastError = value != null ? $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}" : null;
                }
            }
        }

        private static Color _backgroundColor = Color.White;
        /// <summary>Gets or sets the background color of the graphics window.summary>
        public static string BackgroundColor
        {
            get => _backgroundColor.Name;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _backgroundColor = Color.FromName(value);
                    }
                    if (_drawingPanel != null)
                    {
                        InvokeOnUIThread(() => _drawingPanel.BackColor = _backgroundColor);
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid background color: {value}";
                }
            }
        }

        private static Color _brushColor = Color.Black;
        /// <summary>Gets or sets the current brush color used for filling shapes in the graphics window.</summary>
        public static string BrushColor
        {
            get => _brushColor.Name;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _brushColor = Color.FromName(value);
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid brush color: {value}";
                }
            }
        }

        private static bool _canResize = false;
        /// <summary>
        /// Gets or sets a value indicating whether the graphics window is resizable.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c>, the window can be resized by the user; otherwise, it cannot.
        /// </remarks>
        public static bool CanResize
        {
            get => _canResize;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _canResize = value;
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid resize permission. Expecting boolean: {value}";
                }
            }
        }

        private static Color _penColor = Color.Black;
        /// <summary>Gets or sets the current pen color used for drawing in the graphics window.</summary>
        public static string PenColor
        {
            get => _penColor.Name;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _penColor = Color.FromName(value);
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid pen color: {value}";
                }
            }
        }

        private static int _penWidth = 1;
        /// <summary>Gets or sets the width of the pen used for drawing shapes in the graphics window.</summary>
        public static int PenWidth
        {
            get => _penWidth;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _penWidth = value;
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid pen width: {value}";
                }
            }
        }

        private static bool _fontBold = false;
        /// <summary>
        /// Gets or sets a value indicating whether bold text is used in the graphics window.
        /// </summary>
        public static bool FontBold
        {
            get => _fontBold;
            set
            {
                lock (lockObj)
                {
                    _fontBold = value;
                    _fontName = new Font(_fontName.Name, _fontName.Size, _fontBold ? FontStyle.Bold : FontStyle.Regular);
                }
            }
        }


        private static bool _fontItalic = false;

        /// <summary>Gets or sets a value indicating whether italic text is used in the graphics window.</summary>
        public static bool FontItalic
        {
            get => _fontItalic;
            set
            {
                lock (lockObj)
                {
                    _fontItalic = value;
                    _fontName = new Font(_fontName.Name, _fontName.Size, _fontItalic ? FontStyle.Italic : FontStyle.Regular);
                }
            }
        }

        private static Font _fontName = new("Arial", 12);
        /// <summary>Gets or sets the name of the font used for text rendering in the graphics window.</summary>
        public static string FontName
        {
            get => _fontName.Name;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _fontName = new Font(value, _fontName.Size, _fontName.Style);
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid font name: {value}";
                }
            }
        }

        private static int _fontSize = 12;
        /// <summary>Gets or sets the size of the font used for text rendering in the graphics window.</summary>
        public static int FontSize
        {
            get => _fontSize;
            set
            {
                try
                {
                    lock (lockObj)
                    {
                        _fontSize = value;
                        _fontName = new Font(_fontName.Name, _fontSize, _fontName.Style);
                    }
                    LastError = null;
                }
                catch
                {
                    LastError = $"Invalid font size: {value}";
                }
            }
        }

        private static int _height = 600;
        /// <summary>Gets or sets the height of the graphics window. Setting this property resizes the window to the new height.</summary>
        public static int Height
        {
            get => _height;
            set
            {
                lock (lockObj)
                {
                    _height = value;
                }
                ResizeWindow();
            }
        }


        private static string _lastKey = string.Empty;
        /// <summary>The last key pressed in the graphics window, represented as a string.</summary>
        public static string LastKey => _lastKey;

        private static string _lastText = string.Empty;
        /// <summary>The last text input received from the keyboard, represented as a string.</summary>
        public static string LastText => _lastText;

        private const int _left = 100;

        private static int _mouseX;
        private static int _mouseY;
        /// <summary>The current X-coordinate of the mouse pointer within the graphics window.</summary>
        public static int MouseX => _mouseX;

        /// <summary>The current Y-coordinate of the mouse pointer within the graphics window.</summary>
        public static int MouseY => _mouseY;

        private const int _top = 100;

        private static int _width = 800;
        /// <summary>Gets or sets the width of the graphics window. Setting this property resizes the window to the new width.</summary>
        public static int Width
        {
            get => _width;
            set
            {
                lock (lockObj)
                {
                    _width = value;
                }
                ResizeWindow();
            }
        }

        private static Graphics? _graphics;
        private static Form? _graphicsForm;
        private static Panel? _drawingPanel;

        private static void GraphicsForm_MouseMove(object? sender, MouseEventArgs e)
        {
            lock (lockObj)
            {
                _mouseX = e.X;
                _mouseY = e.Y;
            }
        }

        /// <summary>Displays the graphics window.</summary>
        /// <returns><c>true</c> if the window was shown successfully, otherwise <c>false</c>.</returns>
        public static bool Show()
        {
            try
            {
                if (_graphicsForm == null)
                {
                    InvokeOnUIThread(() =>
                    {
                        _graphicsForm = new Form
                        {
                            Text = "Graphics Window",
                            Size = new Size(Width, Height),
                            FormBorderStyle = _canResize ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog,
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(_left, _top)
                        };

                        _drawingPanel = new Panel { Dock = DockStyle.Fill, BackColor = _backgroundColor };
                        _graphicsForm.Controls.Add(_drawingPanel);
                        _graphicsForm.Show();

                        _graphics = _drawingPanel.CreateGraphics();

                        // Add event handlers for key press, text input, and mouse movement
                        _graphicsForm.KeyDown += GraphicsForm_KeyDown;
                        _graphicsForm.KeyPress += GraphicsForm_KeyPress;
                        _graphicsForm.MouseMove += GraphicsForm_MouseMove;
                    });
                }
                else
                {
                    InvokeOnUIThread(() => _graphicsForm.Show());
                }
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // Key and Mouse Event Handlers
        private static void GraphicsForm_KeyDown(object? sender, KeyEventArgs e)
        {
            lock (lockObj)
            {
                _lastKey = e.KeyCode.ToString();
            }
        }

        private static void GraphicsForm_KeyPress(object? sender, KeyPressEventArgs e)
        {
            lock (lockObj)
            {
                _lastText = e.KeyChar.ToString();
            }
        }

        /// <summary>Hides the graphics window.</summary>
        public static bool Hide()
        {
            try
            {
                InvokeOnUIThread(() => _graphicsForm?.Hide());
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // Drawing methods

        /// <summary>Draws a rectangle on the graphics window at the specified coordinates with the specified dimensions.</summary>
        /// <param name="x">The X-coordinate of the rectangle's top-left corner.</param>
        /// <param name="y">The Y-coordinate of the rectangle's top-left corner.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public static void DrawRectangle(int x, int y, int width, int height)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        using Pen pen = new(_penColor, _penWidth);
                        _graphics?.DrawRectangle(pen, x, y, width, height);
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }

        /// <summary>Fills a rectangle on the graphics window at the specified coordinates with the specified dimensions.</summary>
        /// <param name="x">The X-coordinate of the rectangle's top-left corner.</param>
        /// <param name="y">The Y-coordinate of the rectangle's top-left corner.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public static void FillRectangle(int x, int y, int width, int height)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        using SolidBrush brush = new(_brushColor);
                        _graphics?.FillRectangle(brush, x, y, width, height);
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }

        /// <summary>Draws the specified text at the given coordinates on the graphics window.</summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="x">The X-coordinate of the text's starting position.</param>
        /// <param name="y">The Y-coordinate of the text's starting position.</param>
        public static void DrawText(string text, int x, int y)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        using SolidBrush brush = new(_penColor);
                        _graphics?.DrawString(text, _fontName, brush, x, y);
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }

        private static void ResizeWindow()
        {
            if (_graphicsForm == null) return;

            InvokeOnUIThread(() =>
            {
                _graphicsForm.Size = new Size(_width, _height);
                _graphicsForm.Refresh();
            });
        }

        // Helper method for thread-safe UI invocation
        private static void InvokeOnUIThread(Action action)
        {
            if (_graphicsForm?.InvokeRequired == true)
            {
                _graphicsForm.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
