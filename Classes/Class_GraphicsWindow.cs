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

using System.Drawing;

namespace SmallBasicOpenEditionDll
{
    public static class GraphicsWindow
    {

        // Lock object for thread safety when accessing fields
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
        public static Form? GraphicsForm => _graphicsForm;
        private static Panel? _drawingPanel;
        public static Panel? DrawingPanel => _drawingPanel;

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

        public static bool DrawEllipse(int x, int y, int width, int height)
        {
            try
            {
                using Pen pen = new(_penColor, _penWidth);
                _graphics?.DrawEllipse(pen, x, y, width, height);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool FillEllipse(int x, int y, int width, int height)
        {
            try
            {
                using Brush brush = new SolidBrush(_brushColor);
                _graphics?.FillEllipse(brush, x, y, width, height);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            try
            {
                using Pen pen = new(_penColor, _penWidth);
                Point[] points = [new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)];
                _graphics?.DrawPolygon(pen, points);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool FillTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            try
            {
                using Brush brush = new SolidBrush(_brushColor);
                Point[] points = [new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)];
                _graphics?.FillPolygon(brush, points);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static bool DrawLine(int x1, int y1, int x2, int y2)
        {
            try
            {
                using Pen pen = new(_penColor, _penWidth);
                _graphics?.DrawLine(pen, x1, y1, x2, y2);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
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

        public static void DrawBoundText(int x, int y, int width, string text)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        using SolidBrush brush = new(_penColor);
                        using StringFormat format = new()
                        {
                            FormatFlags = StringFormatFlags.LineLimit, // Limits the text to the width
                            Trimming = StringTrimming.Word // Ensures text is trimmed by word if it doesn't fit
                        };

                        // Define the bounding box for the text, with a large height to allow wrapping
                        RectangleF textRectangle = new(x, y, width, float.MaxValue);

                        _graphics?.DrawString(text, _fontName, brush, textRectangle, format);
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
        }

        public static bool DrawImage(string imageName, int x, int y)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        Image? image = ImageList.GetImageByName(imageName);
                        if (image != null)
                        {
                            _graphics?.DrawImage(image, x, y);
                        }
                        else
                        {
                            LastError = $"Image '{imageName}' not found.";
                        }
                    });
                }
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }


        public static bool DrawResizedImage(string imageName, int x, int y, int width, int height)
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        Image? image = ImageList.GetImageByName(imageName);
                        if (image != null)
                        {
                            _graphics?.DrawImage(image, x, y, width, height);
                        }
                        else
                        {
                            LastError = $"Image '{imageName}' not found.";
                        }
                    });
                }
                LastError = null;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        private static Bitmap? _pixelBuffer;  // A bitmap to manage pixel-level operations

        /// <summary>Initializes or resizes the pixel buffer to match the size of the graphics window.</summary>
        private static void InitializePixelBuffer()
        {
            if (_pixelBuffer == null || _pixelBuffer.Width != _width || _pixelBuffer.Height != _height)
            {
                _pixelBuffer = new Bitmap(_width, _height);
                if (_graphics != null)
                {
                    // Copy the existing graphics into the pixel buffer if necessary
                    using Graphics g = Graphics.FromImage(_pixelBuffer);
                    g.Clear(_backgroundColor);  // Set the initial background color
                }
            }
        }

        public static bool SetPixel(int x, int y, string colorName)
        {
            bool result = false;  // Local variable to store the result

            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        InitializePixelBuffer();  // Ensure the pixel buffer is initialized

                        if (_pixelBuffer != null) // Null check
                        {
                            // Convert color name to a Color object
                            Color color = Color.FromName(colorName);

                            // Set the pixel color in the bitmap
                            _pixelBuffer.SetPixel(x, y, color);

                            // Redraw the updated pixel on the graphics window
                            using Graphics g = Graphics.FromImage(_pixelBuffer);
                            _graphics?.DrawImage(_pixelBuffer, new Rectangle(0, 0, _width, _height));

                            // Operation was successful
                            LastError = null;
                            result = true;
                        }
                        else
                        {
                            LastError = "Pixel buffer returned null.";
                            result = false;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                result = false;
            }

            return result;  // Return the result after the lambda is executed
        }


        public static string GetPixel(int x, int y)
        {
            try
            {
                lock (lockObj)
                {
                    if (_pixelBuffer == null) return "Unknown";

                    // Get the pixel color from the bitmap
                    Color color = _pixelBuffer.GetPixel(x, y);

                    // Return the color's name as a string
                    return color.Name;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "Unknown";
            }
        }

        public static string? GetRandomColor()
        {
            try
            {
                int r = Math.GetRandomNumber(257) - 1;  // Generate random value for Red (0-255)
                int g = Math.GetRandomNumber(257) - 1;  // Generate random value for Green (0-255)
                int b = Math.GetRandomNumber(257) - 1;  // Generate random value for Blue (0-255)

                Color randomColor = Color.FromArgb(r, g, b);
                return randomColor.Name;  // Return the name of the random color
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;  // Return a fallback color in case of an error
            }
        }

        public static string? GetColorFromRGB(int red, int green, int blue)
        {
            try
            {
                // Ensure values are in the valid range (0-255)
                red = System.Math.Clamp(red, 0, 255);
                green = System.Math.Clamp(green, 0, 255);
                blue = System.Math.Clamp(blue, 0, 255);

                Color color = Color.FromArgb(red, green, blue);

                LastError = null;
                return color.Name;  // Return the name of the constructed color
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;  // Return a fallback color in case of an error
            }
        }

        public static bool Clear()
        {
            try
            {
                lock (lockObj)
                {
                    InvokeOnUIThread(() =>
                    {
                        if (_graphics != null && _drawingPanel != null)
                        {
                            // Clear the drawing area by filling it with the background color
                            _graphics.Clear(_backgroundColor);

                            // Refresh the drawing panel to apply changes
                            _drawingPanel.Refresh();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
            LastError = null;
            return true;
        }

        public static bool ShowMessage(string text, string title)
        {
            try
            {
                InvokeOnUIThread(() =>
                {
                    // Show the message box with the specified text and title
                    MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }

            LastError = null;
            return true;
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
