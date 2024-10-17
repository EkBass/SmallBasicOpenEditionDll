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
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    public static class Shapes
    {
        private static Dictionary<string, Control> shapes = [];
        private static Dictionary<string, float> shapeRotations = [];
        private static int shapeCounter = 0;
        private static Form? graphicsForm = GraphicsWindow.graphicsForm;

        // Backing field for LastError
        private static string? _lastError;

        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError
        {
            get => _lastError;
            private set
            {
                // Add a timestamp in "yyyy-MM-dd HH:mm:ss" format before the error message
                _lastError = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {value}";
            }
        }

        /// <summary>Adds a rectangle to the GraphicsWindow with the specified width and height.</summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>The name of the created rectangle shape.</returns>
        public static string? AddRectangle(int width, int height)
        {
            try
            { 
                using Panel rectangle = new() { Size = new Size(width, height), Name = "Shape" + shapeCounter++ };
                rectangle.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, rectangle.Name, width, height);
                    e.Graphics.FillRectangle(Brushes.Blue, 0, 0, width, height);
                };

                graphicsForm?.Controls.Add(rectangle);
                shapes[rectangle.Name] = rectangle;
                shapeRotations[rectangle.Name] = 0;  // Initial rotation is 0 degrees
                LastError = null;
                return rectangle.Name;
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
            try
            {
                Panel ellipse = new() { Size = new Size(width, height), Name = "Shape" + shapeCounter++ };
                ellipse.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, ellipse.Name, width, height);
                    e.Graphics.FillEllipse(Brushes.Green, 0, 0, width, height);
                };
                graphicsForm?.Controls.Add(ellipse);
                shapes[ellipse.Name] = ellipse;
                shapeRotations[ellipse.Name] = 0;
                LastError = null;
                return ellipse.Name;
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
        public static string? AddTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            try
            {
                Panel triangle = new() { Size = new Size((int)Math.Max(x1, Math.Max(x2, x3)), (int)Math.Max(y1, Math.Max(y2, y3))), Name = "Shape" + shapeCounter++ };
                triangle.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, triangle.Name, triangle.Width, triangle.Height);
                    e.Graphics.FillPolygon(Brushes.Red, new Point[] { new(x1, y1), new(x2, y2), new(x3, y3) });
                };

                graphicsForm?.Controls.Add(triangle);
                shapes[triangle.Name] = triangle;
                shapeRotations[triangle.Name] = 0;
                LastError = null;
                return triangle.Name;
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
            try
            {
                Panel line = new() { Size = new Size((int)Math.Max(x1, x2), (int)Math.Max(y1, y2)), Name = "Shape" + shapeCounter++ };
                line.Paint += (sender, e) =>
                {
                    ApplyRotation(e.Graphics, line.Name, line.Width, line.Height);
                    e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
                };

                graphicsForm?.Controls.Add(line);
                shapes[line.Name] = line;
                shapeRotations[line.Name] = 0;
                LastError = null;
                return line.Name;
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
                Image image = ImageList.GetImageByName(imageName);
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
        private static void ApplyRotation(Graphics g, string shapeName, int width, int height)
        {
            if (shapeRotations.ContainsKey(shapeName) && shapeRotations[shapeName] != 0)
            {
                float angle = shapeRotations[shapeName];
                g.TranslateTransform(width / 2, height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-width / 2, -height / 2);
            }
        }
    }
}
