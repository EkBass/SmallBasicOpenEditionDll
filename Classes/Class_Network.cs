/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C# .NET 8.0
 * File: Class_Network.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * License: See license.txt
 * 
 * Description:
 * Provides methods to perform network operations, such as downloading files and retrieving web page contents.
 */

using System.IO;
using System.Net.Http;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods to perform network operations, such as downloading files and retrieving web page contents.
    /// </summary>
    public static class Network
    {
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


        // HttpClient instance (should be reused for better performance)
        private static readonly HttpClient httpClient = new();

        /// <summary>Downloads a file from the specified URL and saves it to the provided file path.</summary>
        /// <param name="fileName">The path where the downloaded file will be saved.</param>
        /// <param name="url">The URL of the file to download.</param>
        /// <returns>The path to the saved file, or an empty string if the download fails.</returns>
        public static string? DownloadFile(string fileName, string url) => DownloadFile2(fileName, url).GetAwaiter().GetResult();

        private static async Task<string?> DownloadFile2(string fileName, string url)
        {
            try
            {
                // Download the file as a stream
                using (Stream fileStream = await httpClient.GetStreamAsync(url))
                using (FileStream fs = new(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Copy the file stream to the file
                    await fileStream.CopyToAsync(fs);
                }

                LastError = null;
                return fileName;  // Return the path to the downloaded file
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;  // Returning null in case of an exception
            }
        }


        /// <summary>Retrieves the contents of a web page as a string.</summary>
        /// <param name="url">The URL of the web page to retrieve.</param>
        /// <returns>The contents of the web page as a string, or an empty string if the request fails.</returns>
        public static string? GetWebPageContents(string url)
        {
            return GetWebPageContents2(url).GetAwaiter().GetResult();
        }
        private static async Task<string?> GetWebPageContents2(string url)
        {
            try
            {
                // Download the page contents as a string
                LastError = null;
                return await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }
    }
}
