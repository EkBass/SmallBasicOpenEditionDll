/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Network.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 12th October 2024
 * License: See license.txt
 * 
 * Description:
 * Provides methods to perform network operations, such as downloading files and retrieving web page contents.
 */

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods to perform network operations, such as downloading files and retrieving web page contents.
    /// </summary>
    public static class Network
    {
        // HttpClient instance (should be reused for better performance)
        private static readonly HttpClient httpClient = new();

        private static async Task<string> DownloadFile2(string fileName, string url)
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

                return fileName;  // Return the path to the downloaded file
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to download file: " + ex.Message);
                return "";
            }
        }

        public static async Task<string> GetWebPageContents2(string url)
        {
            try
            {
                // Download the page contents as a string
                return await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve web page: " + ex.Message);
                return "";
            }
        }

        /// <summary>Downloads a file from the specified URL and saves it to the provided file path.</summary>
        /// <param name="fileName">The path where the downloaded file will be saved.</param>
        /// <param name="url">The URL of the file to download.</param>
        /// <returns>The path to the saved file, or an empty string if the download fails.</returns>
        public static dynamic DownloadFile(dynamic fileName, dynamic url)
        {
            dynamic content = DownloadFile2((string)fileName, (string)url).GetAwaiter().GetResult();
            return content;
        }


        /// <summary>Retrieves the contents of a web page as a string.</summary>
        /// <param name="url">The URL of the web page to retrieve.</param>
        /// <returns>
        /// The contents of the web page as a string, 
        /// or an empty string if the request fails.
        /// </returns>
        public static dynamic GetWebPageContents(dynamic url)
        {
            dynamic content = GetWebPageContents2((string)url).GetAwaiter().GetResult();
            return content;
        }
    }
}
