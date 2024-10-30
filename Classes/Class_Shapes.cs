/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Shapes.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to create and manipulate shapes in the GraphicsWindow, including adding rectangles, ellipses, triangles, lines, and images
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    public static class Shapes
    {
        private static readonly Dictionary<string, Control> shapes = [];
        private static readonly Dictionary<string, float> shapeRotations = [];
        private static int shapeCounter = 0;
        private static readonly Form? graphicsForm = GraphicsWindow.GraphicsForm;
        // Dictionary to store the opacity values for each shape (default to 100)
        private static readonly Dictionary<string, float> shapeOpacities = [];
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


        /// <summary>Adds a rectangle to the GraphicsWindow with the specified width and height.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>The name of the created rectangle shape.</returns>
        /// <summary>Adds a rectangle to the GraphicsWindow with the specified width and height.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>The name of the created rectangle shape.</returns>
        public static string? AddRectangle(int width, int height)
        {
            if (GraphicsWindow.DrawingPanel == null) return null;

            try
            {
                string shapeName = "Shape" + shapeCounter++;
                Panel rectangle = new() { Size = new Size(width, height), Name = shapeName };
                rectangle.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, shapeName, width, height);
                    e.Graphics.FillRectangle(Brushes.Blue, 0, 0, width, height);
                };

                GraphicsWindow.DrawingPanel.Controls.Add(rectangle);
                shapes[shapeName] = rectangle;
                shapeRotations[shapeName] = 0;
                LastError = null;
                return shapeName;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds an ellipse to the GraphicsWindow with the specified width and height.</summary>
        /// <param name="width">The width of the ellipse.</param>
        /// <param name="height">The height of the ellipse.</param>
        /// <returns>The name of the created ellipse shape.</returns>
        public static string? AddEllipse(int width, int height)
        {
            if (GraphicsWindow.DrawingPanel == null) return null;

            try
            {
                string shapeName = "Shape" + shapeCounter++;
                Panel ellipse = new() { Size = new Size(width, height), Name = shapeName };
                ellipse.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, shapeName, width, height);
                    e.Graphics.FillEllipse(Brushes.Green, 0, 0, width, height);
                };

                GraphicsWindow.DrawingPanel.Controls.Add(ellipse);
                shapes[shapeName] = ellipse;
                shapeRotations[shapeName] = 0;
                LastError = null;
                return shapeName;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds a triangle to the GraphicsWindow defined by three sets of coordinates.</summary>
        /// <param name="x1">X coordinate of the first point.</param>
        /// <param name="y1">Y coordinate of the first point.</param>
        /// <param name="x2">X coordinate of the second point.</param>
        /// <param name="y2">Y coordinate of the second point.</param>
        /// <param name="x3">X coordinate of the third point.</param>
        /// <param name="y3">Y coordinate of the third point.</param>
        /// <returns>The name of the created triangle shape.</returns>
        /// <summary>Adds a triangle to the GraphicsWindow defined by three sets of coordinates.</summary>
        public static string? AddTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if (GraphicsWindow.DrawingPanel == null) return null;

            try
            {
                string shapeName = "Shape" + shapeCounter++;
                Panel triangle = new()
                {
                    Size = new Size((int)Math.Max(x1, Math.Max(x2, x3)), (int)Math.Max(y1, Math.Max(y2, y3))),
                    Name = shapeName
                };

                triangle.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, shapeName, triangle.Width, triangle.Height);
                    e.Graphics.FillPolygon(Brushes.Red, new Point[] { new(x1, y1), new(x2, y2), new(x3, y3) });
                };

                GraphicsWindow.DrawingPanel.Controls.Add(triangle);
                shapes[shapeName] = triangle;
                shapeRotations[shapeName] = 0;
                LastError = null;
                return shapeName;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds a line to the GraphicsWindow defined by two sets of coordinates.</summary>
        /// <param name="x1">X coordinate of the start point.</param>
        /// <param name="y1">Y coordinate of the start point.</param>
        /// <param name="x2">X coordinate of the end point.</param>
        /// <param name="y2">Y coordinate of the end point.</param>
        /// <returns>The name of the created line shape.</returns>
        public static string? AddLine(int x1, int y1, int x2, int y2)
        {
            if (GraphicsWindow.DrawingPanel == null) return null;

            try
            {
                string shapeName = "Shape" + shapeCounter++;
                Panel line = new() { Size = new Size((int)Math.Max(x1, x2), (int)Math.Max(y1, y2)), Name = shapeName };

                line.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, shapeName, line.Width, line.Height);
                    e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
                };

                GraphicsWindow.DrawingPanel.Controls.Add(line);
                shapes[shapeName] = line;
                shapeRotations[shapeName] = 0;
                LastError = null;
                return shapeName;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Adds an image to the GraphicsWindow using the specified image name from the ImageList.</summary>
        /// <param name="imageName">The name of the image in the ImageList.</param>
        /// <returns>The name of the created image shape.</returns>
        public static string? AddImage(string imageName)
        {
            try
            {
                Image? image = ImageList.GetImageByName(imageName);
                if (image != null)
                {
                    PictureBox pictureBox = new() { Image = image, Size = image.Size, Name = "Shape" + shapeCounter++ };
                    graphicsForm?.Controls.Add(pictureBox);
                    shapes[pictureBox.Name] = pictureBox;
                    shapeRotations[pictureBox.Name] = 0;
                    LastError = null;
                    return pictureBox.Name;
                }
                else
                {
                    LastError = $"Image with name {imageName} not found.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Rotates the specified shape or image by the given angle.</summary>
        /// <param name="shapeName">The name of the shape or image to rotate.</param>
        /// <param name="angle">The angle in degrees to rotate.</param>
        public static bool Rotate(string shapeName, float angle)
        {
            if (shapes.ContainsKey(shapeName))
            {
                if (shapes[shapeName] is PictureBox pictureBox)  // If it's an image
                {
                    pictureBox.Image = RotateImage(pictureBox.Image, angle);
                    LastError = null;
                }
                else  // If it's a shape
                {
                    shapeRotations[shapeName] += angle;  // Update the rotation angle
                    shapes[shapeName].Invalidate();  // Redraw the shape
                    LastError = null;
                }
                return true;
            }
            else
            {
                LastError = $"Shape with name {shapeName} not found.";
                return false;
            }
        }

        /// <summary>Resizes (zooms) the specified shape by scaling its width and height.</summary>
        /// <param name="shapeName">The name of the shape to resize.</param>
        /// <param name="scaleX">The scale factor for the width.</param>
        /// <param name="scaleY">The scale factor for the height.</param>
        public static bool Zoom(string shapeName, int scaleX, int scaleY)
        {
            if (shapes.ContainsKey(shapeName))
            {
                LastError = null;
                shapes[shapeName].Size = new Size(shapes[shapeName].Width * scaleX, shapes[shapeName].Height * scaleY);
                return true;
            }
            else
            {
                LastError = $"Shape with name {shapeName} not found.";
                return false;
            }
        }

        /// <summary>Rotates the specified image by the given angle.</summary>
        /// <param name="img">The image to rotate.</param>
        /// <param name="rotationAngle">The angle in degrees to rotate.</param>
        /// <returns>The rotated image.</returns>
        private static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));

            gfx.Dispose();
            return bmp;
        }

        /// <summary>Applies the rotation to non-image shapes before drawing them.</summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="shapeName">The name of the shape being drawn.</param>
        /// <param name="width">The width of the shape.</param>
        /// <param name="height">The height of the shape.</param>
        private static bool ApplyRotation(Graphics g, string shapeName, int width, int height)
        {
            if (shapeRotations.ContainsKey(shapeName) && shapeRotations[shapeName] != 0)
            {
                float angle = shapeRotations[shapeName];
                g.TranslateTransform(width / 2, height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-width / 2, -height / 2);
                return true;
            }
            return false;
        }

        /// <summary>Adds a text to the GraphicsWindow.</summary>
        public static string? AddText(string text)
        {
            try
            {
                Label label = new() { Text = text, Name = "Shape" + shapeCounter++ };
                graphicsForm?.Controls.Add(label);
                shapes[label.Name] = label;
                shapeRotations[label.Name] = 0;
                LastError = null;
                return label.Name;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>Sets the text of an existing text shape.</summary>
        public static bool SetText(string shapeName, string text)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }
            if (shapes.TryGetValue(shapeName, out var control) && control is Label label)
            {
                label.Text = text;
                LastError = null;
                return true;
            }
            LastError = $"Shape with name {shapeName} not found or is not a text shape.";
            return false;
        }

        /// <summary>Removes a shape from the GraphicsWindow.</summary>
        public static bool Remove(string shapeName)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }
            if (shapes.TryGetValue(shapeName, out var control))
            {
                graphicsForm?.Controls.Remove(control);
                shapes.Remove(shapeName);
                shapeRotations.Remove(shapeName);
                LastError = null;
                return true;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return false;
        }

        /// <summary>Moves a shape to the specified x, y coordinates.</summary>
        public static bool Move(string shapeName, int x, int y)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }
            if (shapes.TryGetValue(shapeName, out var control))
            {
                control.Location = new Point(x, y);
                LastError = null;
                return true;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return false;
        }

        /// <summary>Animates a shape to a new position over a specified duration.</summary>
        public static bool Animate(string shapeName, int x, int y, int duration)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }

            if (shapes.TryGetValue(shapeName, out var control))
            {
                // Create a timer for animation
                var atimer = new System.Timers.Timer(duration / 100); // Timer with interval for each step

                int stepX = (x - control.Left) / 100; // Calculate step increments for X
                int stepY = (y - control.Top) / 100; // Calculate step increments for Y
                int steps = 100; // Number of steps in the animation

                atimer.Elapsed += (s, e) =>
                {
                    if (steps-- > 0) // Decrement the steps counter
                    {
                        // Move the control step by step
                        control.Invoke((MethodInvoker)(() =>
                        {
                            control.Left += stepX;
                            control.Top += stepY;
                        }));
                    }
                    else
                    {
                        // Stop and dispose the timer when animation is done
                        atimer.Stop();
                        atimer.Dispose();
                    }
                };

                atimer.Start(); // Start the timer to begin the animation
                LastError = null;
                return true;
            }
            else
            {
                LastError = $"Shape with name {shapeName} not found.";
                return false;
            }
        }


        /// <summary>Gets the left coordinate of the shape.</summary>
        public static int? GetLeft(string shapeName)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return -1;
            }

            if (shapes.TryGetValue(shapeName, out var control))
            {
                LastError = null;
                return control.Left;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return -1;
        }

        /// <summary>Gets the top coordinate of the shape.</summary>
        public static int? GetTop(string shapeName)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return -1;
            }

            if (shapes.TryGetValue(shapeName, out var control))
            {
                LastError = null;
                return control.Top;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return -1;
        }

        /// <summary>Gets the opacity of a shape.</summary>
        public static float? GetOpacity(string shapeName)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return -1;
            }

            if (shapes.TryGetValue(shapeName, out Control? control))
            {
                // Check if the shape has a tracked opacity value
                if (shapeOpacities.TryGetValue(shapeName, out var opacity))
                {
                    LastError = null;
                    return opacity; // Return the stored opacity value (0-100)
                }

                // Default opacity is 100 (fully opaque)
                return 100f;
            }

            LastError = $"Shape with name {shapeName} not found.";
            return -1;
        }

        /// <summary>Sets the opacity of a shape.</summary>
        public static bool SetOpacity(string shapeName, float opacity)
        {
            // Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }

            // Validate opacity range
            if (opacity < 0 || opacity > 100)
            {
                LastError = "Opacity must be between 0 and 100.";
                return false;
            }

            // Check if the shape exists in the dictionary
            if (shapes.TryGetValue(shapeName, out var control))
            {
                // Store the opacity value
                shapeOpacities[shapeName] = opacity;

                // Apply the opacity (only applicable for image shapes like PictureBox)
                if (control is PictureBox pb)
                {
                    if (pb.Image != null)
                    {
                        pb.Image = SetImageOpacity(pb.Image, opacity / 100f); // Apply opacity
                    }
                    else
                    {
                        LastError = "The image for the PictureBox is null.";
                        return false;
                    }
                }

                LastError = null;
                return true;
            }

            LastError = $"Shape with name {shapeName} not found.";
            return false;
        }

        /// <summary>Applies an opacity level to an image.</summary>
        private static Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new(image.Width, image.Height);
            Graphics gfx = Graphics.FromImage(bmp);

            ColorMatrix colorMatrix = new()
            {
                Matrix33 = opacity // Set the opacity level (alpha channel)
            };

            ImageAttributes attributes = new();
            attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            gfx.Dispose();

            return bmp;
        }


        /// <summary>Hides the shape with the specified name.</summary>
        public static bool HideShape(string shapeName)
        {
			// Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }
			
            if (shapes.TryGetValue(shapeName, out var control))
            {
                control.Visible = false;
                LastError = null;
                return true;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return false;
        }

        /// <summary>Shows the shape with the specified name.</summary>
        public static bool ShowShape(string shapeName)
        {
			// Check if the shapeName is null or empty
            if (string.IsNullOrEmpty(shapeName))
            {
                LastError = "Shape name cannot be null or empty.";
                return false;
            }
            if (shapes.TryGetValue(shapeName, out var control))
            {
                control.Visible = true;
                LastError = null;
                return true;
            }
            LastError = $"Shape with name {shapeName} not found.";
            return false;
        }
    }
}
