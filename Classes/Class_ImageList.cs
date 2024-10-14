/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_ImageList.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * ImageList class provides methods to load, store, and retrieve images from local file paths or URLs. 
 * It maintains an internal dictionary (images) that associates each image with a unique name, allowing efficient access to the stored images.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmallBasicOpenEditionDll
{
    public static class ImageList
    {
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

        // Dictionary to store loaded images
        private static Dictionary<string, Image> images = new();
        private static int imageCounter = 0;

        // HttpClient instance (should be reused for better performance)
        private static readonly HttpClient httpClient = new();

        /// <summary>Loads an image from a file path or a URL.</summary>
        /// <param name="fileNameOrUrl">The file path or URL of the image to load.</param>
        /// <returns>The name of the image, which can be used to reference the loaded image in other methods, or an empty string if the image could not be loaded.</returns>
        public static string LoadImage(string fileNameOrUrl)
        {
            return LoadImageAsync(fileNameOrUrl).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously loads an image from a file path or a URL.
        /// </summary>
        /// <param name="fileNameOrUrl">The file path or URL of the image to load.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the name of the loaded image, or an empty string on failure.</returns>
        private static async Task<string> LoadImageAsync(string fileNameOrUrl)
        {
            LastError = null;
            try
            {
                Image image;

                if (Uri.IsWellFormedUriString(fileNameOrUrl, UriKind.Absolute))
                {
                    // Load the image from a URL
                    byte[] data = await httpClient.GetByteArrayAsync(fileNameOrUrl);
                    using var stream = new MemoryStream(data);
                    image = Image.FromStream(stream);
                }
                else
                {
                    // Load the image from a local file
                    image = Image.FromFile(fileNameOrUrl);
                }

                // Create a unique image name
                string imageName = "Image" + imageCounter++;
                images[imageName] = image;
                return imageName;
            }
            catch (Exception ex)
            {
                // Set LastError on failure
                LastError = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>Retrieves the width of the stored image.</summary>
        /// <param name="imageName">The name of the image to retrieve its width.</param>
        /// <returns>The width of the image in pixels.</returns>
        public static int GetWidthOfImage(string imageName)
        {
            if (!images.TryGetValue(imageName, out Image? image))
            {
                LastError = $"Image with name {imageName} not found.";
                return -1;
            }
            LastError = null;
            return image.Width;
        }

        /// <summary>Retrieves the height of the stored image.</summary>
        /// <param name="imageName">The name of the image to retrieve its height.</param>
        /// <returns>The height of the image in pixels.</returns>
        /// <exception cref="ArgumentException">Thrown when the specified image name is not found.</exception>
        public static int GetHeightOfImage(string imageName)
        {
            if (!images.TryGetValue(imageName, out Image? image))
            {
                LastError = $"Image with name {imageName} not found.";
                return -1;
            }
            LastError = null;
            return image.Height;
        }

        /// <summary>
        /// Retrieves the image object by its name.
        /// This is done since class Shapes has a method that needs this.
        /// </summary>
        /// <param name="imageName">The name of the image.</param>
        /// <returns>The image object associated with the given name, or null if not found.</returns>
        public static Image? GetImageByName(string imageName)
        {
            if (!images.TryGetValue(imageName, out Image? image))
            {
                LastError = $"Image with name {imageName} not found.";
                return null;
            }
            LastError = null;
            return image;
        }

    }
}
