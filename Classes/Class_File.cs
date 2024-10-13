/* 
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
using System.Linq;


namespace SmallBasicOpenEditionDll
{
    /// <summary>Provides methods for file and directory operations, such as reading, writing, copying, and deleting files.</summary>
    public static class File
    {
        /// <summary>Stores the last error message, if any operation fails.</summary>
        public static string? LastError { get; private set; }

        /// <summary>Reads the entire contents of a file.</summary>
        /// <param name="filePath">The file path to read from.</param>
        /// <returns>The contents of the file as a string, or an empty string if an error occurs.</returns>
        public static dynamic ReadContents(dynamic filePath)
        {
            try
            {
                LastError = "";
                return System.IO.File.ReadAllText((string)filePath);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return "";
            }
        }

        /// <summary>Writes the specified contents to a file, overwriting the file if it exists.</summary>
        /// <param name="filePath">The file path to write to.</param>
        /// <param name="contents">The contents to write to the file.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic WriteContents(dynamic filePath, dynamic contents)
        {
            try
            {
                LastError = "";
                System.IO.File.WriteAllText((string)filePath, (string)contents);
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>Reads a specific line from a file.</summary>
        /// <param name="filePath">The file path to read from.</param>
        /// <param name="lineNumber">The line number to read (1-based).</param>
        /// <returns>The line contents as a string, or an empty string if an error occurs.</returns>
        public static dynamic ReadLine(dynamic filePath, dynamic lineNumber)
        {
            filePath = (string)filePath;
            lineNumber = (int)lineNumber;

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

        /// <summary>Writes a specific line in the file.</summary>
        /// <param name="filePath">The file path to write to.</param>
        /// <param name="lineNumber">The line number to write (1-based).</param>
        /// <param name="contents">The contents to write to the specified line.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic WriteLine(dynamic filePath, dynamic lineNumber, dynamic contents)
        {
            filePath = (string)(filePath);
            lineNumber = (int)lineNumber;
            contents = (string)(contents);

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

        /// <summary>Inserts a specific line in the file without overwriting existing lines.</summary>
        /// <param name="filePath">The file path to write to.</param>
        /// <param name="lineNumber">The line number to insert the new line (1-based).</param>
        /// <param name="contents">The contents to insert at the specified line.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic InsertLine(dynamic filePath, dynamic lineNumber, dynamic contents)
        {
            filePath = (string)(filePath);
            lineNumber = (int)lineNumber;
            contents = (string)(contents);

            try
            {
                LastError = "";
                var lines = System.IO.File.ReadAllLines(filePath).ToList(); // Convert to list to allow insertions

                if (lineNumber >= 1 && lineNumber <= lines.Count + 1) // Allow insertion at the end as well
                {
                    lines.Insert(lineNumber - 1, contents); // Insert the new line at the specified position
                    System.IO.File.WriteAllLines(filePath, lines); // Write the updated lines back to the file
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



        /// <summary>Appends contents to the end of a file.</summary>
        /// <param name="filePath">The file path to append to.</param>
        /// <param name="contents">The contents to append.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic AppendContents(dynamic filePath, dynamic contents)
        {
            filePath = (string)filePath;
            contents = (string)contents;

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

        /// <summary>Copies a file from the source path to the destination path.</summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="destinationFilePath">The destination file path.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic CopyFile(dynamic sourceFilePath, dynamic destinationFilePath)
        {
            sourceFilePath = (string)sourceFilePath;
            destinationFilePath = (string)destinationFilePath;

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

        /// <summary>Deletes the specified file.</summary>
        /// <param name="filePath">The file path to delete.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic DeleteFile(dynamic filePath)
        {
            filePath = (string)(filePath);

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

        /// <summary>Creates a new directory at the specified path.</summary>
        /// <param name="directoryPath">The directory path to create.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic CreateDirectory(dynamic directoryPath)
        {
            directoryPath = (string)directoryPath;
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

        /// <summary>Deletes the specified directory and all its contents.</summary>
        /// <param name="directoryPath">The directory path to delete.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static dynamic DeleteDirectory(dynamic directoryPath)
        {
            directoryPath = (string)directoryPath;
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

        /// <summary>Retrieves all file paths in the specified directory.</summary>
        /// <param name="directoryPath">The directory path to search.</param>
        /// <returns>An array of file paths, or an empty array if an error occurs.</returns>
        public static dynamic GetFiles(dynamic directoryPath)
        {
            directoryPath = (string)directoryPath;
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


        /// <summary>Retrieves all directory paths in the specified directory.</summary>
        /// <param name="directoryPath">The directory path to search.</param>
        /// <returns>An array of directory paths, or an empty array if an error occurs.</returns>
        public static dynamic GetDirectories(dynamic directoryPath)
        {
            directoryPath = (string)directoryPath;

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
