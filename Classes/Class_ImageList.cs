/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_ImageList.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * ImageList class provides methods to load, store, and retrieving images from local file paths or URLs. 
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
    /// <summary>Provides methods to load, store, and retrieve images from local files or URLs.</summary>
    public static class ImageList
    {
        // Dictionary to store loaded images
        private static Dictionary<string, Image> images = [];
        private static int imageCounter = 0;

        // HttpClient instance (should be reused for better performance)
        private static readonly HttpClient httpClient = new();

        /// <summary>Loads an image from a file path or a URL.</summary>
        /// <param name="fileNameOrUrl">The file path or URL of the image to load.</param>
        /// <returns>
        /// The name of the image, which can be used to reference the loaded image in other methods,
        /// or an empty string if the image could not be loaded.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the provided file path or URL is invalid.</exception>
        public static dynamic LoadImage(dynamic fileNameOrUrl) => LoadImageAsync((string)fileNameOrUrl).GetAwaiter().GetResult();

        private static async Task<string> LoadImageAsync(string fileNameOrUrl)
        {
            try
            {
                Image image;

                // Check if it's a URL and load from the web
                if (Uri.IsWellFormedUriString(fileNameOrUrl, UriKind.Absolute))
                {
                    byte[] data = await httpClient.GetByteArrayAsync(fileNameOrUrl);
                    using var stream = new MemoryStream(data);
                    image = Image.FromStream(stream);
                }
                else
                {
                    // Load from a local file
                    image = Image.FromFile(fileNameOrUrl);
                }

                // Create a unique image name
                string imageName = "Image" + imageCounter++;
                images[imageName] = image;

                return imageName;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Image with name {fileNameOrUrl} not found.");
            }
        }


        /// <summary>Retrieves the width of the stored image.</summary>
        /// <param name="imageName">The name of the image to retrieve its width.</param>
        /// <returns>The width of the image in pixels.</returns>
        /// <exception cref="ArgumentException">Thrown when the specified image name is not found.</exception>
        public static dynamic GetWidthOfImage(dynamic imageName) => !images.TryGetValue((string)imageName, out Image? value)
                ? throw new ArgumentException($"Image with name {imageName} not found.")
                : value.Width;


        /// <summary>Retrieves the height of the stored image.</summary>
        /// <param name="imageName">The name of the image to retrieve its height.</param>
        /// <returns>The height of the image in pixels.</returns>
        /// <exception cref="ArgumentException">Thrown when the specified image name is not found.</exception>
        public static dynamic GetHeightOfImage(dynamic imageName)
        {
            return images.TryGetValue((string)imageName, out Image? value)
                ? (dynamic)value.Height
                : throw new ArgumentException($"Image with name {imageName} not found.");
        }

        /// <summary>Retrieves the image by its name.</summary>
        /// <param name="imageName">The name of the image to retrieve.</param>
        /// <returns>The <see cref="Image"/> object associated with the specified name.</returns>
        /// <exception cref="ArgumentException">Thrown when the specified image name is not found.</exception>
        public static Image GetImageByName(dynamic imageName)
        {
            return images.TryGetValue((string)imageName, out Image? value)
                ? value
                : throw new ArgumentException($"Image with name {imageName} not found.");
        }
    }
}
