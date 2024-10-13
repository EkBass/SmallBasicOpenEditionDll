/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Desktop.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * The Desktop class enables retrieving the dimensions of the primary screen and allows setting the desktop wallpaper.
 * The class uses SystemParametersInfo function from user32.dll to change the wallpaper.
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods and properties for interacting with the desktop environment,
    /// such as retrieving the desktop dimensions and setting the wallpaper.
    /// </summary>
    public static class Desktop
    {
        // User32.dll import for setting wallpaper
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        /// <summary>Gets the width of the primary screen (desktop) in pixels.</summary>
        public static dynamic Width => Screen.PrimaryScreen != null ? Screen.PrimaryScreen.Bounds.Width : 0;

        /// <summary>Gets the height of the primary screen (desktop) in pixels.</summary>
        public static dynamic Height => Screen.PrimaryScreen != null ? Screen.PrimaryScreen.Bounds.Height : 0;

        /// <summary>Sets the desktop wallpaper to the specified file path or URL.</summary>
        public static void SetWallpaper(dynamic fileOrUrl)
        {
            fileOrUrl = (string)fileOrUrl;

            if (!string.IsNullOrWhiteSpace(fileOrUrl))
            {
                int result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fileOrUrl, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

                if (result == 0)
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                throw new ArgumentException("File path or URL cannot be null or empty.", nameof(fileOrUrl));
            }
        }
    }
}
