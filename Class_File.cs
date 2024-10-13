﻿/* 
 * Project: SmallBasicOpenEditionDll
 * Language: C#
 * File: Class_File.cs
 * Author: Kristian Virtanen, krisu.virtanen@gmail.com
 * Last date: 13th October 2024
 * License: See license.txt
 * 
 * Description:
 * File class provides a set of methods for performing common file and directory operations.
 * Class is designed to simplify file operations by wrapping them in easy-to-use methods.
 * */

using System;
using System.IO;
using System.Collections.Generic;


namespace SmallBasicOpenEditionDll
{
    /// <summary>
    /// Provides methods for file and directory operations, such as reading, writing, copying, and deleting files.
    /// </summary>
    public static class File
    {
        /// <summary>
        /// Stores the last error message, if any operation fails.
        /// </summary>
        public static string? LastError { get; private set; }

        /// <summary>
        /// Reads the entire contents of a file.
        /// </summary>
        /// <param name="filePath">The file path to read from.</param>
        /// <returns>The contents of the file as a string, or an empty string if an error occurs.</returns>
        public static string ReadContents(string filePath)
        {
            try
            {
                LastError = "";
                return System.IO.File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// Writes the specified contents to a file, overwriting the file if it exists.
        /// </summary>
        /// <param name="filePath">The file path to write to.</param>
        /// <param name="contents">The contents to write to the file.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool WriteContents(string filePath, string contents)
        {
            try
            {
                LastError = "";
                System.IO.File.WriteAllText(filePath, contents);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Reads a specific line from a file.
        /// </summary>
        /// <param name="filePath">The file path to read from.</param>
        /// <param name="lineNumber">The line number to read (1-based).</param>
        /// <returns>The line contents as a string, or an empty string if an error occurs.</returns>
        public static string ReadLine(string filePath, int lineNumber)
        {
            try
            {
                LastError = "";
                var lines = System.IO.File.ReadAllLines(filePath);
                return lineNumber >= 1 && lineNumber <= lines.Length ? lines[lineNumber - 1] : "";
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// Writes a specific line in the file.
        /// </summary>
        /// <param name="filePath">The file path to write to.</param>
        /// <param name="lineNumber">The line number to write (1-based).</param>
        /// <param name="contents">The contents to write to the specified line.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool WriteLine(string filePath, int lineNumber, string contents)
        {
            try
            {
                LastError = "";
                var lines = System.IO.File.ReadAllLines(filePath);
                if (lineNumber >= 1 && lineNumber <= lines.Length)
                {
                    lines[lineNumber - 1] = contents;
                    System.IO.File.WriteAllLines(filePath, lines);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Inserts a line into the file at the specified line number.
        /// </summary>
        /// <param name="filePath">The file path to insert the line into.</param>
        /// <param name="lineNumber">The line number to insert at (1-based).</param>
        /// <param name="contents">The contents to insert.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool InsertLine(string filePath, int lineNumber, string contents)
        {
            try
            {
                LastError = "";
                var lines = new List<string>(System.IO.File.ReadAllLines(filePath));
                if (lineNumber >= 1 && lineNumber <= lines.Count + 1)
                {
                    lines.Insert(lineNumber - 1, contents);
                    System.IO.File.WriteAllLines(filePath, [.. lines]);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Appends contents to the end of a file.
        /// </summary>
        /// <param name="filePath">The file path to append to.</param>
        /// <param name="contents">The contents to append.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool AppendContents(string filePath, string contents)
        {
            try
            {
                LastError = "";
                System.IO.File.AppendAllText(filePath, contents + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Copies a file from the source path to the destination path.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="destinationFilePath">The destination file path.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                LastError = "";
                System.IO.File.Copy(sourceFilePath, destinationFilePath, true);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="filePath">The file path to delete.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool DeleteFile(string filePath)
        {
            try
            {
                LastError = "";
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Creates a new directory at the specified path.
        /// </summary>
        /// <param name="directoryPath">The directory path to create.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool CreateDirectory(string directoryPath)
        {
            try
            {
                LastError = "";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Deletes the specified directory and all its contents.
        /// </summary>
        /// <param name="directoryPath">The directory path to delete.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool DeleteDirectory(string directoryPath)
        {
            try
            {
                LastError = "";
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Retrieves all file paths in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The directory path to search.</param>
        /// <returns>An array of file paths, or an empty array if an error occurs.</returns>
        public static string GetFiles(string directoryPath)
        {
            try
            {
                LastError = "";
                // Get all files and join them into a single string, with newline separators
                string[] files = Directory.GetFiles(directoryPath);
                return string.Join(Environment.NewLine, files);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "";
            }
        }


        /// <summary>
        /// Retrieves all directory paths in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The directory path to search.</param>
        /// <returns>An array of directory paths, or an empty array if an error occurs.</returns>
        public static string GetDirectories(string directoryPath)
        {
            try
            {
                string[] files = Directory.GetDirectories(directoryPath);
                return string.Join(Environment.NewLine, files);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "";
            }
        }
    }
}
