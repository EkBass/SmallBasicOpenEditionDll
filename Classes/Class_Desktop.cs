﻿/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_Desktop.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
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
    public static class Desktop
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

        // User32.dll import for setting wallpaper
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        /// <summary>Gets the width of the primary screen (desktop) in pixels.</summary>
        public static int Width => Screen.PrimaryScreen != null ? Screen.PrimaryScreen.Bounds.Width : 0;

        /// <summary>Gets the height of the primary screen (desktop) in pixels.</summary>
        public static int Height => Screen.PrimaryScreen != null ? Screen.PrimaryScreen.Bounds.Height : 0;

        /// <summary>Sets the desktop wallpaper to the specified file path or URL.</summary>
        public static bool SetWallpaper(string fileOrUrl) 
        {
            LastError = null;

            // Check for null or empty file path or URL
            if (string.IsNullOrWhiteSpace(fileOrUrl))
            {
                LastError = "File path or URL cannot be null or empty.";
                return false;
            }

            // Attempt to set the wallpaper using SystemParametersInfo
            int result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fileOrUrl, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

            // If result is 0, the operation failed
            if (result == 0)
            {
                int errorCode = Marshal.GetLastWin32Error();
                LastError = $"Failed to set wallpaper. Win32 Error Code: {errorCode} - {new System.ComponentModel.Win32Exception(errorCode).Message}";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
